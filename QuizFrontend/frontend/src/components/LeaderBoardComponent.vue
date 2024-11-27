<template>
 <div class="body">
    <form class="d-flex-8">
              <input class="form-control mr-sm-2" type="search" placeholder="Search" aria-label="Search" v-model="searchBoard">
              <button @click="searchBoardByName" class="btn btn-outline-success my-2 my-sm-0">Search</button>
    </form>
    <main>
      
      <div id="header">
        <h1 class="leader-board-name">{{ currLeaderBoard.leaderBoardName }}</h1>
        <div class="leader-board-header">
        <span class="starting">Starting from:{{ formatDate(currLeaderBoard.startingFrom) }}</span>
        <span class="ending">Ending:{{ formatDate(currLeaderBoard.ending) }}</span>
      </div>
        <!-- <button class="share">
          <i class="ph ph-share-network"></i>
        </button> -->
      </div>
      <div id="leaderboard" >
        <div class="ribbon"></div>
        <table>
            <th>
                <tr>
                    <td class="number">Rank</td>
                     <td class="accuracy">Image</td>
                     <a @click="sortLeaderBoard1" >
                      <td class="name" id="1">Name <svg width="20px" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 384 512"><path d="M214.6 41.4c-12.5-12.5-32.8-12.5-45.3 0l-160 160c-12.5 12.5-12.5 32.8 0 45.3s32.8 12.5 45.3 0L160 141.2 160 448c0 17.7 14.3 32 32 32s32-14.3 32-32l0-306.7L329.4 246.6c12.5 12.5 32.8 12.5 45.3 0s12.5-32.8 0-45.3l-160-160z"/></svg></td>
                      
                    </a>                     
                     <td class="accuracy">Accuracy</td>
                     <a @click="sortLeaderBoard1" >
                      <td class="points" id="0">Score <svg width="20px" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 384 512"><path d="M214.6 41.4c-12.5-12.5-32.8-12.5-45.3 0l-160 160c-12.5 12.5-12.5 32.8 0 45.3s32.8 12.5 45.3 0L160 141.2 160 448c0 17.7 14.3 32 32 32s32-14.3 32-32l0-306.7L329.4 246.6c12.5 12.5 32.8 12.5 45.3 0s12.5-32.8 0-45.3l-160-160z"/></svg></td>
                    </a>       
                     
                </tr>
            </th>
           <tr v-for="(scoreCard,index) in currScoreCard" :key="scoreCard.id">
            <td class="number">{{ currScoreCardPage*scoresPerpage - scoresPerpage + ++index}}</td>
            <td class="accuracy" v-if="scoreCard.image == '' ">
              <img src="user.jpg" class="image-holder"/>
            </td>
            <td class="accuracy" v-if="scoreCard.image != '' ">
              <img :src="`${url}${scoreCard.image}`" class="image-holder"/>
            </td>
            <td class="name" style="text-transform: capitalize;">{{scoreCard.username}}</td>
            <td >{{scoreCard.acuuracy }}%</td>      
            <td class="points" v-if="currScoreCardPage*scoresPerpage - scoresPerpage + index == 1">
              {{ scoreCard.score }}<img class="gold-medal" src="gold.png" alt="gold medal"/>
            </td>
            <td class="points" v-if="currScoreCardPage*scoresPerpage - scoresPerpage + index == 2">
              {{ scoreCard.score }}<img class="gold-medal" src="silver.png" alt="gold medal"/>
            </td>
            <td class="points" v-if="currScoreCardPage*scoresPerpage - scoresPerpage + index == 3">
              {{ scoreCard.score }}<img class="gold-medal" src="bronze.png" alt="gold medal"/>
            </td>
          </tr>
        
        </table>
        <div class="pagination">
              <button 
                class="page-btn page-step" 
                :class="{ dontshow: currScoreCardPage <= 1 }" 
                @click="changePageScoreCard(currScoreCardPage-1)">
                «
              </button>
              <a 
                v-for="page in pages" 
                :key="page.id" 
                class="page-btn" 
                :class="{ active: currScoreCardPage === page +1}" 
                @click="changePageScoreCard(page)">
                {{ ++page }}
              </a>
              <button 
                class="page-btn page-step" 
                :class="{ dontshow: currentPage >= pages.length }" 
                @click="changePageScoreCard(currScoreCardPage+1)">
                »
              </button>
            </div>
        <!-- <div id="buttons">
          <button class="exit">Exit</button>
          <button class="continue">Continue</button>
        </div> -->
      </div>
      <div v-if="leaderBoard.length==0">
        Leaderboard not found
      </div>
    </main>
    <a
    class=" page-step left" 
    :class="{ inactive :currentPage <= 1}" 
    @click="changePage(--currentPage)">
    <svg fill="white" width="40px" height="40px" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512"><path d="M41.4 233.4c-12.5 12.5-12.5 32.8 0 45.3l160 160c12.5 12.5 32.8 12.5 45.3 0s12.5-32.8 0-45.3L109.3 256 246.6 118.6c12.5-12.5 12.5-32.8 0-45.3s-32.8-12.5-45.3 0l-160 160zm352-160l-160 160c-12.5 12.5-12.5 32.8 0 45.3l160 160c12.5 12.5 32.8 12.5 45.3 0s12.5-32.8 0-45.3L301.3 256 438.6 118.6c12.5-12.5 12.5-32.8 0-45.3s-32.8-12.5-45.3 0z"/></svg>
  </a>
  <!-- <a 
    v-for="(question, index) in leaderBoard.length" 
    :key="question.id" 
    class="page-btn" 
    :class="{ active: currentPage === index + 1}" 
    @click="changePage(index + 1)">
    {{ index + 1 }}
  </a> -->
  <a 
    class=" page-step right" 
    :class="{inactive :currentPage >= leaderBoard.length  }" 
    @click="changePage(++currentPage)">
    <svg fill="white" width="40px" height="40px" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512"><path d="M470.6 278.6c12.5-12.5 12.5-32.8 0-45.3l-160-160c-12.5-12.5-32.8-12.5-45.3 0s-12.5 32.8 0 45.3L402.7 256 265.4 393.4c-12.5 12.5-12.5 32.8 0 45.3s32.8 12.5 45.3 0l160-160zm-352 160l160-160c12.5-12.5 12.5-32.8 0-45.3l-160-160c-12.5-12.5-32.8-12.5-45.3 0s-12.5 32.8 0 45.3L210.7 256 73.4 393.4c-12.5 12.5-12.5 32.8 0 45.3s32.8 12.5 45.3 0z"/></svg>
   </a>
  </div>
  
</template>
<script>
import { getLeaderBoard } from '@/scripts/LeaderBoardService';
import { getLeaderBoardByName } from '@/scripts/ProfileService';
import { sortLeaderBoard } from '@/scripts/LeaderBoardService';
import { toast } from 'vue3-toastify';
import 'vue3-toastify/dist/index.css';

export default{

    name:'LeaderBoardComponent',
    data(){
       return{
        pages:[],
        order:1,
        leaderBoard:[],
        paginatedScoreCard:[],
        currLeaderBoard:[],
        currScoreCard:[],
        currScoreCardPage:1,
        scoresPerpage:5,
        currentPage:1,
        searchBoard:'',
        url:"http://localhost:5193",
        formatDate(datetime) {
         const date = new Date(datetime);
         return date.toLocaleDateString();
         },
         sortLeaderBoard1(event){
            console.log(event.target.id)
            let choice = event.target.id;
            console.log(this.currLeaderBoard.leaderBoardId)
            sortLeaderBoard(parseInt(choice),this.currLeaderBoard.leaderBoardId,this.order)
            .then(response =>{
               this.paginatedScoreCard = response.data.scoreCards
               console.log(response.data.scoreCards);
               if(this.order == 0){
                 this.order =1;
               }
               else{
                this.order = 0;
               }
            })
            this.pages.length = 0;
            let size = Math.ceil(this.paginatedScoreCard.length/this.scoresPerpage);

              for(let i=0;i<size;i++){
                 this.pages.push(i);
              }
              this.currScoreCard = this.paginatedScoreCard.slice(this.currScoreCardPage*this.scoresPerpage- this.scoresPerpage,this.currScoreCardPage*this.scoresPerpage)

         },
         changePage(page) {
                 console.log(page);
                 this.currentPage = page;
                 this.pages.length = 0;
                 this.currScoreCardPage = 1;
                 this.currLeaderBoard = this.leaderBoard[page - 1];
                 this.paginatedScoreCard = this.currLeaderBoard.scoreCards
                 let size = Math.ceil(this.paginatedScoreCard.length/this.scoresPerpage);

              for(let i=0;i<size;i++){
                 this.pages.push(i);
              }
              this.currScoreCard = this.paginatedScoreCard.slice(this.currScoreCardPage*this.scoresPerpage- this.scoresPerpage,this.currScoreCardPage*this.scoresPerpage)
         },
         changePageScoreCard(index){
            console.log(index)
             this.currScoreCardPage = index;
             this.currScoreCard = this.paginatedScoreCard.slice(index*this.scoresPerpage- this.scoresPerpage,index*this.scoresPerpage);
         },
         searchBoardByName(){
                event.preventDefault();
                getLeaderBoardByName(this.searchBoard)
                .then(response =>{
                    
                    if(response.data.length>1){
                      this.currLeaderBoard = response.data
                      this.leaderBoard = this.currLeaderBoard;
                    }
                    else if(response.data.length==1){
                      this.currLeaderBoard = response.data
                      this.currLeaderBoard = this.currLeaderBoard[0]
                      this.paginatedScoreCard = this.currLeaderBoard.scoreCards
                      let size = Math.ceil(this.paginatedScoreCard.length/this.scoresPerpage);
                      this.pages.length =0;
                      for(let i=0;i<size;i++){
                        this.pages.push(i);
                      }
                      this.currScoreCard = this.paginatedScoreCard.slice(this.currScoreCardPage*this.scoresPerpage- this.scoresPerpage,this.currScoreCardPage*this.scoresPerpage)
                    }
                    else{
                       toast.error("No leaderboard found for the search",{
                         autoClose:4000
                       })
                    }
                    
                    console.log(response.data)
                })

            },  
       }    
    },  
    created(){
       getLeaderBoard()
       .then(response => {
         console.log(response.data);
         this.leaderBoard = response.data;
         this.currLeaderBoard= this.leaderBoard[0]
         this.paginatedScoreCard = this.currLeaderBoard.scoreCards
         let size = Math.ceil(this.paginatedScoreCard.length/this.scoresPerpage);

         for(let i=0;i<size;i++){
            this.pages.push(i);
         }
         this.currScoreCard = this.paginatedScoreCard.slice(this.currScoreCardPage*this.scoresPerpage- this.scoresPerpage,this.currScoreCardPage*this.scoresPerpage)
       })
       .catch(error => {
         console.log(error.response.data)
       });
    }
}

</script>

<style scoped>
* {
  font-size: 62, 5%;
  box-sizing: border-box;
  margin: 0;
}

.leader-board-header{
    display: flex;
    width: 100%;
    justify-content: space-between;
}
.d-flex-8{
  display:flex;
  gap: 8px;
  position: absolute;
  top:0%;
  width: 35rem;
}
.image-holder{
    width: 60px;
    height: 60px;
    border-radius: 50%;
}
.left{
    position: absolute;
    left: 8%;
}
.inactive{
  display: none
}

.right{
    position: absolute;
    right: 0%;
}
.accuracy{
font-family: Sour Gummy !important;
}
.leader-board-name{
  font-family: Sour Gummy;
}
.body {
  height: 100%;
  position: absolute;
  top: 16%;
  width: 100%;
  min-height: 100vh;
  background-color:#8585de !important;
  display: flex;
  align-items: center;
  justify-content: center;
}
.starting,.ending{
    height: 40px;
    width: 45%;
    padding: 4px;
    display: flex;
    align-items: center;
    justify-content: center;
    border-radius: 12px;
}

.starting{
  background: aliceblue;
}
.ending{
  background-color: rgb(241, 102, 102);
  color: white;
}

main {
  width: 40rem;
  top: 8%;
  position: absolute;
  height: 43rem;
  background-color: #ffffff;
  -webkit-box-shadow: 0px 5px 15px 8px #e4e7fb;
  box-shadow: 0px 5px 15px 8px #e4e7fb;
  display: flex;
  flex-direction: column;
  align-items: center;
  border-radius: 0.5rem;
}

#header {
  width: 100%;
  flex-direction: column;
  gap: 4px;
  font-family: Sour Gummy;
  font-size: 20px;
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 2.5rem 2rem;
}

.share {
  width: 4.5rem;
  height: 3rem;
  background-color: #f55e77;
  border: 0;
  border-bottom: 0.2rem solid #c0506a;
  border-radius: 2rem;
  cursor: pointer;
}

.share:active {
  border-bottom: 0;
}

.share i {
  color: #fff;
  font-size: 2rem;
}

h1 {
  font-family: "Rubik", sans-serif;
  font-size: 1.7rem;
  color: #141a39;
  text-transform: uppercase;
  cursor: default;
}

#leaderboard {
  width: 100%;
  position: relative;
}

table {
  width: 100%;
  border-collapse: collapse;
  table-layout: fixed;
  color: #141a39;
  cursor: default;
}

tr {
  transition: all 0.2s ease-in-out;
  border-radius: 0.2rem;
}

tr:not(:first-child):hover {
  background-color: #fff;
  transform: scale(1.1);
  -webkit-box-shadow: 0px 5px 15px 8px #e4e7fb;
  box-shadow: 0px 5px 15px 8px #e4e7fb;
}

tr:nth-child(odd) {
  background-color: #f9f9f9;
}

tr:nth-child(1) {
  color: #fff;
  cursor: pointer;
}

td {
  font-family: "Rubik", sans-serif;
  font-size: 1.2rem;
  padding: 1rem 2rem;
  position: relative;
}

.number {
  width: 1rem;
  font-size: 2rem;
  font-weight: bold;
  text-align: left;
  font-family: Sour Gummy !important;
}

.name {
  text-align: left;
  font-size: 1.2rem;
  font-family: Sour Gummy !important;
  display: flex;
  fill: white;
  gap: 4px;
}

.points {
  font-weight: bold;
  font-size: 1.2rem;
  font-family: Sour Gummy !important;
  justify-content: flex-end;
  align-items: center;
  display: flex;
  fill: white;
  gap: 4px;
}


.gold-medal {
  height: 3rem;
  margin-left: 1.5rem;
}

.ribbon {
  width: 42rem;
  height: 5.5rem;
  top: -0.5rem;
  background-color: #5c5be5;
  position: absolute;
  left: -1rem;
  -webkit-box-shadow: 0px 15px 11px -6px #7a7a7d;
  box-shadow: 0px 15px 11px -6px #7a7a7d;
}

.ribbon::before {
  content: "";
  height: 1.5rem;
  width: 1.5rem;
  bottom: -0.8rem;
  left: 0.35rem;
  transform: rotate(45deg);
  background-color: #5c5be5;
  position: absolute;
  z-index: -1;
}

.ribbon::after {
  content: "";
  height: 1.5rem;
  width: 1.5rem;
  bottom: -0.8rem;
  right: 0.35rem;
  transform: rotate(45deg);
  background-color: #5c5be5;
  position: absolute;
  z-index: -1;
}

#buttons {
  width: 100%;
  margin-top: 3rem;
  display: flex;
  justify-content: center;
  gap: 2rem;
}

.exit {
  width: 11rem;
  height: 3rem;
  font-family: "Rubik", sans-serif;
  font-size: 1.3rem;
  text-transform: uppercase;
  color: #7e7f86;
  border: 0;
  background-color: #fff;
  border-radius: 2rem;
  cursor: pointer;
}

.exit:hover {
  border: 0.1rem solid #5c5be5;
}

.continue {
  width: 11rem;
  height: 3rem;
  font-family: "Rubik", sans-serif;
  font-size: 1.3rem;
  color: #fff;
  text-transform: uppercase;
  background-color: #5c5be5;
  border: 0;
  border-bottom: 0.2rem solid #3838b8;
  border-radius: 2rem;
  cursor: pointer;
}

.continue:active {
  border-bottom: 0;
}

.dontshow{
  display: none;
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


@media (max-width: 740px) {
    * {
      font-size: 70%;
    }
}

@media (max-width: 500px) {
    * {
      font-size: 55%;
    }
}

@media (max-width: 390px) {
    * {
      font-size: 45%;
    }
}
</style>