<template>
    <div class="profile-holder-main">
    
         <div class="table-container">
          <h1>Reports</h1>
           <!-- <form class="search-btn">
              <input class="form-control mr-sm-2" type="search" placeholder="Search" aria-label="Search" v-model="searchQuiz">
              <button @click="searchQuizByTitle" class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
           </form> -->
  
          <table class="scorecard-table">
              <thead>
                  <tr>
                      <th>SI NO</th>
                      <th>Reporter Id</th>
                      <th>Type</th>
                      <th>Description</th>
                      <th>Status</th>
                      <th>Actions</th>
                  </tr>
              </thead>
              <tbody>
                  <tr v-for="(scoreCard,index ) in scoreCards" :key="scoreCard.id">
                      <td>{{ ++index }}</td>
                      <td>{{ scoreCard.reportedBy }}</td>
                      <td>{{ scoreCard.queryType }}</td>
                      <td>{{ scoreCard.description }}</td>
                      <td v-if="scoreCard.isResolved">Completed</td>                
                      <td v-if="!scoreCard.isResolved">Pending</td>                   
                      <td>
                        <button class="btn btn-warning" data-toggle="modal" data-target="#exampleModals" @click="addQueryID(scoreCard.queryId)">Update</button>
                        <button class="btn btn-danger"  @click="deleteQuery(scoreCard.queryId)">Delete</button></td>
                     
                  </tr>
              </tbody>
          </table>
          <div class="responsive-div">
             <div class="smaller-view-port"  v-for="(scoreCard,index ) in scoreCards" :key="scoreCard.id" >
                 <p>SI NO: {{ ++index  }}</p>
                 <p>Reporter ID: {{ scoreCard.reportedBy  }}</p>
                 <p>Query Type: {{ scoreCard.queryType  }}</p>
                 <p>Description: {{ ++index  }}</p>
                 <p>Status: {{ (scoreCard.isResolved ? "Completed" : "Pending")  }}</p>
                 <p>Actions: <button class="btn btn-warning" data-toggle="modal" data-target="#exampleModals">Update</button>
                    <button class="btn btn-danger"  @click="deleteQuery(scoreCard.reportedBy)">Delete</button></p>
             </div>
          </div>
              <div class="pagination">
                <button 
                  class="page-btn page-step" 
                  :class="{ dontshow: currentPage <= 1 }" 
                  @click="changePage(currentPage-1)">
                  «
                </button>
                <a 
                  v-for="page in pages" 
                  :key="page.id" 
                  class="page-btn" 
                  :class="{ active: currentPage === page +1}" 
                  @click="changePage(page)">
                  {{ ++page }}
                </a>
                <button 
                  class="page-btn page-step" 
                  :class="{ dontshow: currentPage >= pages.length }" 
                  @click="changePage(currentPage+1)">
                  »
                </button>
              </div>
      </div>
    </div>
    <div class="modal fade" id="exampleModals" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="exampleModalLabel1">Do you want to update</h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <label for="description">Is resolved</label>
          <input type="checkbox" name="description" v-model="description" value="true" @click="oncheck">       
        </div>
        <div class="modal-footer">
          <button type="button"  class="btn btn-danger" data-dismiss="modal">Go back</button>
          <button type="button" class="btn btn-primary"  @click="updateQuery" data-dismiss="modal">Submit</button>
        </div>
      </div>
    </div>
    </div>
  </template>
  <script>
  import { getAllQuery,deleteQuery,updateQuery  } from '@/scripts/QueryService';
  import { submitQuery } from '@/scripts/QuizService';
  import { toast } from 'vue3-toastify';
  import 'vue3-toastify/dist/index.css';
  
  export default{
      name:'ReportComponent',
      data(){
         return{
          map:{
            "true":true,
            "false":false
          },
           userId:sessionStorage.getItem('Id'),
           type:'',
           description:'',
           queryId:'',
           reportedBy:'',
           scoreCards:[],
           paginatedScoreCard:[],
           currentPage:1,
           pages:[],
           scoresPerpage:5
         }
      },
      methods:{
        addQueryID(queryId){
            this.queryId = parseInt(queryId);
        },
        oncheck(event){
           this.isResolved = this.map[event.target.value]
        },
        addQuery(){
                 submitQuery(this.userId,this.type,this.description)
                 .then(response =>{
                   toast.success("Query added succesfully",{
                    autoClose:4000
                   })
                   console.log(response.data)
                 }).catch((err) =>{
                    console.log(err)
                    if(err.response.data.errors.Description ){
                      toast.error("Description cannot be empty",{
                      autoClose:4000
                     })
                    }
                    if(err.response.data.errors.QueryType){
                      toast.error("Query type cannot be empty",{
                      autoClose:4000
                     })
                    }
                     toast.error("Could not add query try after sometime",{
                     autoClose:4000
                   })
                 })
              },
       getAllquery(){
           getAllQuery()
           .then(response => {
            this.paginatedScoreCard = response.data
            let size = Math.ceil(this.paginatedScoreCard.length/this.scoresPerpage);
            this.scoreCards = this.paginatedScoreCard.slice(this.currentPage* this.scoresPerpage - this.scoresPerpage , this.currentPage * this.scoresPerpage)
             this.pages.length = 0; 
            for(let i=0;i<size;i++){
               this.pages.push(i);
             }
             console.log(response.data);
           })
       },
       deleteQuery(id){
         deleteQuery(id)
         .then( (response)=>{
           toast.success("Deleted succesfully",{
            autoClose:4000
           })
           console.log(response);
           this.getAllquery();
         })
       },
       updateQuery(){
         let query = this.paginatedScoreCard.find((query)=>{
           return query.queryId == this.queryId; 
         })
         this.description = query.description;
         this.type = query.queryType;
         this.reportedBy = query.reportedBy;
         console.log(query.queryId,this.reportedBy,this.type,this.description)
          updateQuery(query.queryId,this.reportedBy,this.type,this.description)
          .then(response =>{
            toast.success("Query updated",{
              autoClose:4000
            })
            console.log(response);
            this.getAllquery()
          })
          .catch((err)=>{
             console.log(err)
          })
       },
       changePage(index){
           this.currentPage = index;
           this.scoreCards = this.paginatedScoreCard.slice(index*this.scoresPerpage- this.scoresPerpage,index*this.scoresPerpage);
           console.log(this.scoreCards)
        },
        // inputChange(event){
        //   let image = event.target.files[0]
        //   updateImage(sessionStorage.getItem('Name'),image)
        //   .then(response =>{
        //     sessionStorage.setItem('Image',response.data.imagePath)
        //     this.image = "http://localhost:5193"+response.data.imagePath;
        //     console.log(this.image)
        //   })
          
        // }
      },
      mounted(){
        console.log("scorecard")
        this.getAllquery();
        console.log(this.scoreCards)
      },
  }
  </script>
  
  <style scoped>
  .responsive-div{
    display: none;
  }
  body {
      margin: 0;
      padding: 0;
      background-color: #f4f4f9;
  }
  .page-btn.active {
    color: #ffffff;
    background-color: #1b95ff;
  }
  .pagination {
    width: 100%;
    padding: 0 1rem;
    margin-top: 1.5rem;
    display: flex;
    justify-content: center;
  }
  .pagination:not(:has(.page-btn:target)) .page-step[data-shown="1"] {
    display: inline-flex;
  }
  .pagination:not(:has(.page-btn:target)) #page-1 {
    color: #ffffff;
    background-color: #1b95ff;
  }
  .page-btn {
    color: #000000;
    width: 2.5rem;
    height: 2.5rem;
    margin-right: 0.25rem;
    display: inline-flex;
    flex-shrink: 0;
    justify-content: center;
    align-items: center;
  }
  .modal-header {
    justify-content: space-between;
  }
  .modal-body{
    display: flex;
      width: 80%;
      flex-direction: column;
      margin: auto;
  }
  
  .page-btn:last-child {
    margin-right: 0;
  }
  .page-btn:is(a) {
    text-decoration: none;
    background-color: #ffffff;
    border-radius: 50%;
    cursor: pointer;
    transition: color 128ms ease-out, background-color 128ms ease-out;
  }
  .page-btn:is(a):not(:target):hover, .page-btn:is(a):not(:target):focus, .page-btn:is(a):not(:target):active {
    background-color: #dfdfdf;
  }
  .page-btn:is(a):target {
    color: #ffffff;
    background-color: #1b95ff;
  }
  
  .dontshow{
    display: none;
  }
  
    .profile-holder{
     width: 80%;
     display: flex;
      margin: 20px auto;
      background: white;
      border-radius: 8px;
      min-height: 50vh;
      box-shadow: 4px 8px 26px 4px;
  
   }
  
  
  .table-container {
      font-family: Sour Gummy;
      max-width: 1200px;
      margin: 50px auto;
      padding: 20px;
      background-color: #fff;
      box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
      border-radius: 10px;
  }
  .page-btn.active {
    color: #ffffff;
    background-color: #1b95ff;
  }
  
  h1 {
      text-align: center;
      color: #333;
  }
  
  .scorecard-table {
      width: 100%;
      border-collapse: collapse;
      margin-top: 20px;
  }
  
  .scorecard-table thead tr {
      background-color: #007BFF;
      color: #fff;
  }
  
  .scorecard-table th,
  .scorecard-table td {
      text-align: center;
      padding: 15px;
      border: 1px solid #ddd;
  }
  
  .scorecard-table tbody tr:nth-child(odd) {
      background-color: #f9f9f9;
  }
  
  .scorecard-table tbody tr:hover {
      background-color: #f1f1f1;
  }
  
  .btn-view {
      background-color: #28a745;
      color: #fff;
      border: none;
      padding: 5px 10px;
      cursor: pointer;
      border-radius: 5px;
  }
  
  .btn-delete {
      background-color: #dc3545;
      color: #fff;
      border: none;
      padding: 5px 10px;
      cursor: pointer;
      border-radius: 5px;
  }
  
  .btn-view:hover {
      background-color: #218838;
  }
  
  .btn-delete:hover {
      background-color: #c82333;
  }
  
  .profile-holder-main{
      margin: auto;
      width: 80%;
      left: 10%;
  }
  .search-btn{
      display: flex;
      justify-content: center;
      margin: 20px;
      width: 30%;
     }
  
     .profile-desc,
   .profile-img{
     width: 50%;
   }
   .profile-img{
      display: flex;
      justify-content: center;
      align-items: center;
   }
   .profile-desc{
      display: flex;
      flex-direction: column;
      justify-content: center;
   }
  
   .svg-cam{
      width: 32px;
      position: relative;
      top: 64px;
      right: 32px;
   }
  
   @media screen and (max-width:720px ) {
    .responsive-div{
      display: flex;
      flex-direction: column;
    }
    .scorecard-table{
        display: none;
    }
   }
  </style>