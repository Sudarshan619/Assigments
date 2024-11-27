<template>
  <div class="profile-holder-main">
   <section class="profile-holder">  
            <aside class="profile-desc" style="font-family: Sour Gummy;font-size: 24px;">
                 <p style="text-transform: capitalize;">Username: {{ username }}</p>
                 <p>Role : {{ (role == 0)?'Admin':'Player' }}</p>
                 <p>Email Address: random@gmail.com</p>
            </aside>
           
              <aside class="profile-img" >
                <img style="border-radius: 50%; height: 50%; width: 50%; border: 6px solid rgb(146, 146, 234);" :src="image" />
                <input id="file-input" type="file" style="display: none;" @change="inputChange">
                <svg class="svg-cam" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512" onclick="document.getElementById('file-input').click()">
                  <path d="M149.1 64.8L138.7 96 64 96C28.7 96 0 124.7 0 160L0 416c0 35.3 28.7 64 64 64l384 0c35.3 0 64-28.7 64-64l0-256c0-35.3-28.7-64-64-64l-74.7 0L362.9 64.8C356.4 45.2 338.1 32 317.4 32L194.6 32c-20.7 0-39 13.2-45.5 32.8zM256 192a96 96 0 1 1 0 192 96 96 0 1 1 0-192z"/>
                </svg>
             </aside>  
                
                <!-- <svg class="svg-cam" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512"><path d="M149.1 64.8L138.7 96 64 96C28.7 96 0 124.7 0 160L0 416c0 35.3 28.7 64 64 64l384 0c35.3 0 64-28.7 64-64l0-256c0-35.3-28.7-64-64-64l-74.7 0L362.9 64.8C356.4 45.2 338.1 32 317.4 32L194.6 32c-20.7 0-39 13.2-45.5 32.8zM256 192a96 96 0 1 1 0 192 96 96 0 1 1 0-192z"/></svg> -->
     
       </section>
       <div class="table-container">
        <h1>Your Scorecards</h1>
         <form class="search-btn">
            <input class="form-control mr-sm-2" type="search" placeholder="Search" aria-label="Search" v-model="searchQuiz">
            <button @click="searchQuizByTitle" class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
         </form>

        <table class="scorecard-table">
            <thead>
                <tr>
                    <th>#</th>
                    <th>Username</th>
                    <th>Quiz Name</th>
                    <th>Score</th>
                    <th>Accuracy (%)</th>
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(scoreCard,index ) in scoreCards" :key="scoreCard.id">
                    <td>{{ ++index }}</td>
                    <td>{{ scoreCard.username }}</td>
                    <td>{{ scoreCard.quizName }}</td>
                    <td>{{ scoreCard.score }}</td>                  
                    <td>{{ scoreCard.acuuracy }}</td>                   
                    <td><button class="btn-view">Report</button> <button class="btn-delete">Delete</button></td>
                </tr>
            </tbody>
        </table>
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
</template>
<script>
import { getAllScoreCard ,updateImage} from '@/scripts/ProfileService';

export default{
    name:'UserProfile',
    data(){
       return{
         image:'user.jpg',
         username:'Unknown',
         role :'Admin',
         scoreCards:[],
         paginatedScoreCard:[],
         currentPage:1,
         pages:[],
         scoresPerpage:5
       }
    },
    methods:{
     getAllScoreCard1(){
         getAllScoreCard()
         .then(response => {
           this.paginatedScoreCard = response.data
           this.paginatedScoreCard = this.paginatedScoreCard.filter((score)=>{
             return score.username == sessionStorage.getItem('Name')
           })

          let size = Math.ceil(this.paginatedScoreCard.length/this.scoresPerpage);
          this.scoreCards = this.paginatedScoreCard.slice(this.currentPage* this.scoresPerpage - this.scoresPerpage , this.currentPage * this.scoresPerpage)
           for(let i=0;i<size;i++){
             this.pages.push(i);
           }
           console.log(response.data);
         })
     },
     changePage(index){
         this.currentPage = index;
         this.scoreCards = this.paginatedScoreCard.slice(index*this.scoresPerpage- this.scoresPerpage,index*this.scoresPerpage);
         console.log(this.scoreCards)
      },
      inputChange(event){
        let image = event.target.files[0]
        updateImage(sessionStorage.getItem('Name'),image)
        .then(response =>{
          sessionStorage.setItem('Image',response.data.imagePath)
          this.image = "http://localhost:5193"+response.data.imagePath;
          console.log(this.image)
        })
        
      }
    },
    mounted(){
      console.log("scorecard")
      this.getAllScoreCard1();
      this.image = "http://localhost:5193"+sessionStorage.getItem('Image')
      this.username = sessionStorage.getItem('Name')
      this.role = sessionStorage.getItem('Role')
      console.log(this.image)
    },
}
</script>

<style scoped>
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

</style>