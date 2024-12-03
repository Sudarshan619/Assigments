<template>
    <header class="profile">
      <!-- Profile details -->
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
       </section>
       
       <!-- Delete section -->
       <section>
         <div class="delete-div">
            <h4>Delete section</h4>
            <div class="selection-type">
                <label for="Category">Category</label>
                <select @change="selectCategory" class="category-selection">
                     <option style="display:none">Select option</option>
                     <option value="option">Option</option>
                     <option value="question">Question</option>
                     <option value="leaderboard">LeaderBoard</option>
                     <option value="quiz">Quiz</option>
                     <option value="scorecard">ScoreCard</option>
                </select>
            </div>
            
           
            <!-- Delete question -->
            <form class="create-form" v-if="this.selectedCategory === 'question'">
                <div>
                <section class="create-form w-100" v-if="showSearchResults && searchResults.length > 0">
                   <h5>Available questions</h5>
                   <div v-for="searchResult in searchResults" :key="searchResult.id" class="search-result">
                       <aside>
                           {{ searchResult.questionName }}
                       </aside>
                       <button @click="onSelect" class="btn btn-primary" :value="searchResult.questionId">
                           Select
                       </button>
                   </div>
                   <button @click="hideResults" class="btn btn-secondary">
                      <span aria-hidden="true">&times;</span>
                   </button>
               </section>
              </div>
            <label>Search question</label>
            <form class="d-flex-8">
              <input class="form-control mr-sm-2" type="search" placeholder="Search" aria-label="Search" v-model="searchQuestion">
              <button @click="searchQuestionsDelete" class="btn btn-outline-success my-2 my-sm-0" data-toggle="modal" data-target="#exampleModal" type="submit">Search</button>
            </form>
               <div>
                <label for="QuestionId">Question Id</label>
                <input class="form-control" type="number" id="QuestionId" v-model="QuestionId">
               </div>
               <button class="btn btn-success" type="submit" @click="deleteQuestion1">Delete Question</button>
            </form>
            <!-- Delete option -->
            <form class="create-form" v-if="this.selectedCategory === 'option'">
               <div>
                <label for="QuestionId">Option Id</label>
                <input class="form-control" type="number" id="QuestionId" v-model="optionId">
               </div>
               <button class="btn btn-success" type="submit" @click="deleteOption">Delete Option</button>
            </form>
            <!-- Delete leaderboard -->
            <form class="create-form" v-if="this.selectedCategory === 'leaderboard'">
            <label>Search leaderboard</label>
            <form class="d-flex-8">
              <input class="form-control mr-sm-2" type="search" placeholder="Search" aria-label="Search" v-model="searchBoard">
              <button @click="searchBoardByName" class="btn btn-outline-success my-2 my-sm-0">Search</button>
            </form>
            <section class="create-form w-100" v-if="searchResultBoard.length>0">
                <h5>Available LeaderBoard</h5>
                <div v-for="searchResult in searchResultBoard" :key="searchResult.id" class="search-result">
                    <aside>
                        {{ searchResult.leaderBoardName }}
                    </aside>
                    <button @click="onSelectBoard" class="btn btn-primary" :value="searchResult.leaderBoardId" type="submit">
                        Select
                    </button>
                </div>
               
            </section>
               <div>
                <label for="QuestionId">LeaderBoard Id</label>
                <input class="form-control" type="number" id="QuestionId" v-model="boardId">
               </div>
               <button @click="deleteBoard" class="btn btn-success" type="submit">Delete LeaderBoard</button>
            </form>
            <!-- Delete quiz -->
            <form class="create-form" v-if="this.selectedCategory === 'quiz'">
                <label>Search quiz</label>
                <form class="d-flex-8">
                  <input class="form-control mr-sm-2" type="search" placeholder="Search" aria-label="Search" v-model="searchQuiz">
                  <button @click="searchQuizByTitle" class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
                </form>
                <section class="create-form w-100" v-if="searchResultQuiz.length>0">
                <h5>Available quizes</h5>
                <div v-for="searchResult in searchResultQuiz" :key="searchResult.id" class="search-result">
                    <aside>
                        {{ searchResult.title }}
                    </aside>
                    <button @click="onSelectQuiz" class="btn btn-primary" :value="searchResult.quizId">
                        Select
                    </button>
                </div>            
               </section>
               <div>
                <label for="QuestionId">Quiz Id</label>
                <input class="form-control" type="number" id="QuestionId" v-model="quizId">
               </div>
               <button @click="deleteQuiz" class="btn btn-success" type="submit">Delete Quiz</button>
            </form>
             <!-- Delete scorecard -->
             <form class="create-form" v-if="this.selectedCategory === 'scorecard'">
               <div>
                <label for="QuestionId">Scorecard Id</label>
                <input class="form-control" type="number" id="QuestionId" v-model="ScoreCardId">
               </div>
               <button class="btn btn-success" type="submit" @click="deleteScoreCard">Delete ScoreCard</button>
            </form>
         </div>
         <aside>

         </aside>
          
       </section>

       <!-- Edit section -->
       <section>
        <div class="edit-div">
            <h4>Edit section</h4>
            <div class="selection-type">
                <label for="Category">Category</label>
                <select @change="selectCategory" class="category-selection">
                    <option style="display:none">Select option</option>
                     <!-- <option value="option">Option</option> -->
                     <option value="question">Question</option>
                     <option value="leaderboard">LeaderBoard</option>
                     <option value="quiz">Quiz</option>
                     <!-- <option value="scorecard">ScoreCard</option> -->
                </select>               
            </div>
           
            <!-- Edit question -->
        <div >
         <form class="create-form" v-if="this.selectedCategory === 'question'">
            <label>Search qustion</label>
           <form class="d-flex-8" >
              <input class="form-control mr-sm-2" type="search" placeholder="Search" aria-label="Search" v-model="searchQuestion">
              <button @click="searchQuestions()" class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
           </form>

           <section class="create-form w-100" v-if="showSearchResults && searchResults.length > 0">
                   <h5>Available questions</h5>
                   <div v-for="searchResult in searchResults" :key="searchResult.id" class="search-result">
                       <aside>
                           {{ searchResult.questionName }}
                       </aside>
                       <button @click="onSelect" class="btn btn-primary" :value="searchResult.questionId">
                           Select
                       </button>
                   </div>
                   <button @click="hideResults" class="btn btn-secondary">
                      <span aria-hidden="true">&times;</span>
                   </button>
               </section>
            <section v-else>
                No result found
            </section>
            <div>
                <label for="QuestionId">Question Id</label>
                <input class="form-control" type="number" id="QuestionId" v-model="QuestionId" required/>
            </div>
            <div>
                <label for="QuestionName">Question Text</label>
                <input class="form-control" type="text" id="QuestionName" v-model="QuestionName" required/>
            </div>
            <div style="display: flex;flex-direction: column;">
                <label for="Category">Difficulty</label>
                <select @change="onclickDifficulty" class="category-selection">
                     <option style="display:none">Select option</option>
                     <option value="easy">Easy</option>
                     <option value="medium">Medium</option>
                     <option value="hard">Hard</option>
                </select>               
            </div>
            <div style="display: flex;flex-direction: column;">
                <label for="Category">Category</label>
                <select @change="onclick" class="category-selection">
                     <option style="display:none">Select option</option>
                     <option v-for="category in Categories" :key="category.id" :value="category">{{ category }}</option>
                </select>               
            </div>
            <div>
                <label for="Points">Score</label>
                <input class="form-control" type="number" id="Points" v-model="Points" required/>
            </div>
            <button class="btn btn-success" @click="editQuestion" type="submit">Edit Question</button>
            
        </form>
        </div>

        <!-- Edit quiz -->
        <div>
            <form class="create-form" v-if=" this.selectedCategory === 'quiz'">
                <label>Search quiz</label>
                <form class="d-flex-8">                
                  <input class="form-control mr-sm-2" type="search" placeholder="Search" aria-label="Search" v-model="searchQuiz">
                  <button @click="searchQuizByTitle" class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
                </form>
                <section class="create-form w-100" v-if="showSearchResults && searchResultQuiz.length > 0">
                <h5>Available Quiz</h5>
                <div v-for="searchResult in searchResultQuiz" :key="searchResult.id" class="search-result">
                    <aside>
                        {{ searchResult.title }}
                    </aside>
                    <button @click="onSelectQuiz" class="btn btn-primary" :value="searchResult.quizId">
                        Select
                    </button>
                </div>
                <button @click="hideResults" class="btn btn-secondary">
                      <span aria-hidden="true">&times;</span>
                </button>
               
            </section>
            <div>
                <label for="QuestionName">Quiz Id</label>
                <input class="form-control" type="text" id="QuestionName" v-model="quizId">
            </div>
            <div>
                <label for="QuestionName">Creator Id</label>
                <input class="form-control" type="text" id="QuestionName" v-model="CreatorId">
            </div>
            <div>
                <label for="QuestionName">Qtitle</label>
                <input class="form-control" type="text" id="QuestionName" v-model="title">
            </div>
            <div>
                <label for="QuestionName">Duration</label>
                <input class="form-control" type="number" id="QuestionName" v-model="duration">
            </div>
            <div style="display: flex;flex-direction: column;">
                <label for="Category">Category</label>
                <select @change="onclick" class="category-selection">
                     <option style="display:none">Select option</option>
                     <option v-for="category in Categories" :key="category.id" :value="category">{{ category }}</option>
                </select>               
            </div>
            <div style="display: flex;flex-direction: column;">
                <label for="Category">Difficulty</label>
                <select @change="onclickDifficulty" class="category-selection">
                     <option style="display:none">Select option</option>
                     <option value="easy">Easy</option>
                     <option value="medium">Medium</option>
                     <option value="hard">Hard</option>
                </select>               
            </div>
            <div>
                <label for="Points">No of Questions</label>
                <input class="form-control" type="number" id="Points" v-model="NoOfQuestions">
            </div>
            <button class="btn btn-warning" @click="editQuiz" type="submit">Edit Quiz</button>
            </form>
        </div>

        <!-- Edit leaderboard -->
        <div>
               <form class="create-form" v-if=" this.selectedCategory === 'leaderboard' ">
                <label>Search leaderboard</label>
                <form class="d-flex-8">
                  <input class="form-control mr-sm-2" type="search" placeholder="Search" aria-label="Search" v-model="searchBoard">
                  <button @click="searchBoardByName" class="btn btn-outline-success my-2 my-sm-0">Search</button>
                </form>
                <section class="create-form w-100" v-if="searchResultBoard.length>0">
                <h5>Available leaderboard</h5>
                <div v-for="searchResult in searchResultBoard" :key="searchResult.id" class="search-result">
                    <aside>
                        {{ searchResult.leaderBoardName }}
                    </aside>
                    <button @click="onSelectBoard" class="btn btn-primary" :value="searchResult.leaderBoardId">
                        Select
                    </button>
                </div>
               
            </section>
            <div>
                <label for="Points">LeaderBoard Id</label>
                <input class="form-control" type="number" id="Points" v-model="boardId">
            </div>
            <div>
                <label for="QuestionName">LeaderBoard Title</label>
                <input class="form-control" type="text" id="QuestionName" v-model="LeaderBoardName">
            </div>
            <div style="display: flex;flex-direction: column;">
                <label for="Category">Category</label>
                <select @change="onclick" class="category-selection">
                     <option style="display:none">Select option</option>
                     <option v-for="category in Categories" :key="category.id" :value="category">{{ category }}</option>
                </select>               
            </div>
            <div>
                <label for="Points">Quiz Id</label>
                <input class="form-control" type="number" id="Points" v-model="quizId">
            </div>
            <button class="btn btn-primary" @click="editLeaderBoard" type="submit">Update LeaderBoard</button>
            </form>
        </div>

         </div>
         <aside>

         </aside>
       </section>

       <!-- Create section-->
       <section>
        <div class="create-div">
            <h4 >Create section</h4>
            <div class="selection-type">
                <label for="Category">Category</label>
                <select @change="selectCategory" class="category-selection">
                    <option style="display:none">Select option</option>
                     <option value="option">Option</option>
                     <option value="question">Question</option>
                     <option value="leaderboard">LeaderBoard</option>
                     <option value="quiz">Quiz</option>
                </select>               
            </div>
            
            <!-- Create questions -->
            <form class="create-form" v-if=" this.selectedCategory === 'question' ">
            <div>
                <label for="QuestionName">Question Text</label>
                <input class="form-control" type="text" id="QuestionName" v-model="QuestionName">
            </div>
            <div style="display: flex;flex-direction: column;">
                <label for="Category">Category</label>
                <select @change="onclick" class="category-selection">
                     <option style="display:none">Select option</option>
                     <option v-for="category in Categories" :key="category.id" :value="category">{{ category }}</option>
                </select>               
            </div>
            <div style="display: flex;flex-direction: column;">
                <label for="Category">Difficulty</label>
                <select @change="onclickDifficulty" class="category-selection">
                     <option style="display:none">Select option</option>
                     <option value="easy">Easy</option>
                     <option value="medium">Medium</option>
                     <option value="hard">Hard</option>
                </select>               
            </div>
            <div>
                <label for="Points">Score</label>
                <input class="form-control" type="number" id="Points" v-model="Points">
            </div>
            <button class="btn btn-success" @click="addQuestion" type="submit">Create Question</button>
            </form>

            <!-- Create quiz -->
            <form class="create-form" v-if=" this.selectedCategory === 'quiz' ">
                <form class="d-flex-8">
                  <input class="form-control mr-sm-2" type="search" placeholder="Search" aria-label="Search" v-model="searchUser">
                  <button @click="searchUserByName" class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
                </form>
                <section class="create-form w-100" v-if="searchResultUser.length>0">
                <h5>Available Users</h5>
                <div v-for="searchResult in searchResultUser" :key="searchResult.id" class="search-result">
                    <aside>
                        {{ searchResult.username }}
                    </aside>
                    <button @click="onSelectUser" class="btn btn-primary" :value="searchResult.id">
                        Select
                    </button>
                </div>
               
            </section>
            <div>
                <label for="QuestionName">Creator Id</label>
                <input class="form-control" type="text" id="QuestionName" v-model="CreatorId">
            </div>
            <div>
                <label for="QuestionName">Duration</label>
                <input class="form-control" type="number" id="QuestionName" v-model="duration">
            </div>
            <div>
                <label for="QuestionName">Qtitle</label>
                <input class="form-control" type="text" id="QuestionName" v-model="title">
            </div>
            <div style="display: flex;flex-direction: column;">
                <label for="Category">Category</label>
                <select @change="onclick" class="category-selection">
                     <option style="display:none">Select option</option>
                     <option v-for="category in Categories" :key="category.id" :value="category">{{ category }}</option>
                </select>               
            </div>
            <div style="display: flex;flex-direction: column;">
                <label for="Category">Difficulty</label>
                <select @change="onclickDifficulty" class="category-selection">
                     <option style="display:none">Select option</option>
                     <option value="easy">Easy</option>
                     <option value="medium">Medium</option>
                     <option value="hard">Hard</option>
                </select>               
            </div>
            <div>
                <label for="Points">No of Questions</label>
                <input class="form-control" type="number" id="Points" v-model="NoOfQuestions">
            </div>
            <button class="btn btn-warning" @click="createQuiz" type="submit">Create Quiz</button>
            </form>


            <!-- Create leaderboard -->
            <form class="create-form" v-if=" this.selectedCategory === 'leaderboard' ">
                <form class="d-flex-8">
                  <input class="form-control mr-sm-2" type="search" placeholder="Search" aria-label="Search" v-model="searchQuiz">
                  <button @click="searchQuizByTitle" class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
                </form>
                <section class="create-form w-100" v-if="searchResultQuiz.length>0">
                <h5>Available quizes</h5>
                <div v-for="searchResult in searchResultQuiz" :key="searchResult.id" class="search-result">
                    <aside>
                        {{ searchResult.title }}
                    </aside>
                    <button @click="onSelectQuiz" class="btn btn-primary" :value="searchResult.quizId">
                        Select
                    </button>
                </div>
               
            </section>
            <div>
                <label for="QuestionName">LeaderBoard Title</label>
                <input class="form-control" type="text" id="QuestionName" v-model="LeaderBoardName" required/>
            </div>
            <div style="display: flex;flex-direction: column;">
                <label for="Category">Category</label>
                <select @change="onclick" class="category-selection">
                     <option style="display:none">Select option</option>
                     <option v-for="category in Categories" :key="category.id" :value="category">{{ category }}</option>
                </select>               
            </div>
            <div>
                <label for="Points">Quiz Id</label>
                <input class="form-control" type="number" id="Points" v-model="quizId" required/>
            </div>
            <button class="btn btn-primary" @click="addLeaderBoard" type="submit">Create LeaderBoard</button>
            </form>
            
            <!-- Create options -->
            <form class="create-form" v-if=" this.selectedCategory === 'option'">
            <form class="d-flex-8">
              <input class="form-control mr-sm-2" type="search" placeholder="Search" aria-label="Search" v-model="searchQuestion">
              <button @click="searchQuestions" class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
            </form>
            <section class="create-form w-100" v-if="searchResults.length>0">
                <h5>Available questions</h5>
                <div v-for="searchResult in searchResults" :key="searchResult.id" class="search-result">
                    <aside>
                        {{ searchResult.questionName }}
                    </aside>
                    <button @click="onSelect" class="btn btn-primary" :value="searchResult.questionId">
                        Select
                    </button>
                </div>
               
            </section>
            <div>
                <label for="QuestionName">Question Id</label>
                <input class="form-control" type="text" id="QuestionName" v-model="QuestionId">
            </div>
            <div>
                <label for="Points">Option Text</label>
                <input class="form-control" type="text" id="Points" v-model="optionText">
            </div>
            <div>
                <label for="QuestionName">Is option correct</label>
                <select @change="correctSelection" class="category-selection">
                     <option style="display:none">Select option</option>
                     <option value="true" >True</option>
                     <option value="false" >False</option>
                </select>  
               
            </div>
            <button class="btn btn-success" @click="submitOption" type="submit">Submit Options</button>
            <button class="btn btn-warning" @click="addMoreOption()" >Add More Options</button>
            </form>
            <div class="create-form" v-if="this.selectedCategory === 'option'">
             <h6>Added options (not yet submitted)</h6>
             <div class="search-result">
               <aside>Question Id</aside>
               <aside>Option</aside>
               <aside>Is correct</aside>
               <aside>Delete</aside>
             </div>
             <div v-for="option in options" :key="option.id" class="search-result">
                <aside>
                    {{ option.questionId }}
                </aside>
                <aside>
                    {{ option.text }}
                </aside>
                <aside>
                    {{ option.isCorrect }}
                </aside>
                <aside>
                    <button @click="deleteSelection(option.text)">
                        <svg width="16px" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 384 512"><path d="M342.6 150.6c12.5-12.5 12.5-32.8 0-45.3s-32.8-12.5-45.3 0L192 210.7 86.6 105.4c-12.5-12.5-32.8-12.5-45.3 0s-12.5 32.8 0 45.3L146.7 256 41.4 361.4c-12.5 12.5-12.5 32.8 0 45.3s32.8 12.5 45.3 0L192 301.3 297.4 406.6c12.5 12.5 32.8 12.5 45.3 0s12.5-32.8 0-45.3L237.3 256 342.6 150.6z"/></svg>
                    </button>             
                </aside>
             </div>

            </div>
          
          </div>
       </section>
    </header>
    <footer>

    </footer>
</template>
<script>
import {updateQuiz,addQuiz, updateImage,addQuestion ,addOptions,getAllQuiz,getUserByName,getLeaderBoardByName,addLeaderBoard,deleteQuiz, deleteLeaderBoard, deleteQuestion,updateQuestion, deleteScoreCard, deleteOption, updateLeaderBoard} from '@/scripts/ProfileService';
import { searchQuestions } from '@/scripts/QuestionService';
import { toast } from 'vue3-toastify';
import 'vue3-toastify/dist/index.css';

export default{
    name:'ProfileComponent',
    data(){
        return{
            map:{
              "true": true,
              "false":false
            },
            difficultyMap:{
               "easy":0,
               "medium":1,
               "hard":2
            },
            categoryMap :{
                "GeneralKnowledge":0,
                "Sports":1,
                "Politics":2,
                "Geography":3,
                "History":4
                 },
            image:'user.png',
            username:'Unknown',
            role :'Admin',
            selectedCategory:'question',
            QuestionId:'', 
            optionId:'',
            ScoreCardId:'',
            NoOfQuestions:'',
            title:'',
            CreatorId:'',
            duration:'',     
            Category:'',               
            QuestionName:'',
            LeaderBoardName:'',
            Points:'',  
            difficulty:'',
            searchQuestion:'',
            searchQuiz:'',
            searchUser:'',
            searchBoard:'',
            isCorrect:'false',
            optionText:'',
            quizId:'',
            boardId:'',
            Categories:[],
            searchResultBoard:[],
            searchResultCreate:[],
            searchResultQuiz:[],
            searchResultUser:[],
            searchResults:[],
            options:[],
            showSearchResults: true,
            hideResults() {
            this.showSearchResults = false; // Hides the section
            },
            editQuestion(){
                event.preventDefault()
                console.log(this.Category)
               updateQuestion(this.QuestionId,this.QuestionName,this.difficultyMap[this.difficulty],this.categoryMap[this.Category],this.Points)
               .then(response => {
                 toast.success("Edit succesfull",{
                    autoClose:4000
                 })
                 this.QuestionId ='',
                 this.QuestionName = '',
                 this.difficulty = '',
                 this.Category = '',
                 this.Points = ''
                 this.showSearchResults = false
                 console.log(response)
               })
               .catch(()=>{
                 toast.error("Could not edit check if all the fields are filled",{
                    autoClose:4000
                 })
               })
            },
            editLeaderBoard(){
                event.preventDefault()
                console.log(this.Category)
                console.log(this.boardId,this.LeaderBoardName,this.categoryMap[this.Category],this.quizId)
                updateLeaderBoard(this.boardId,this.LeaderBoardName,this.categoryMap[this.Category],this.quizId)
               .then(response => {
                 toast.success("Edit succesfull for the leaderboard",{
                    autoClose:4000
                 })
                 this.boardId = '',
                 this.LeaderBoardName = '',
                 this.Category = '',
                 this.quizId = '',
                 this.showSearchResults = false
                 console.log(response)
               })
               .catch(()=>{
                 toast.error("Could not edit check if all the fields are filled",{
                    autoClose:4000
                 })
               })
            },
            editQuiz(){
                event.preventDefault();
                console.log(parseInt(this.quizId),parseInt(this.CreatorId),this.categoryMap[this.Category],this.duration,this.title,this.difficultyMap[this.difficulty],this.NoOfQuestions)
                updateQuiz(parseInt(this.quizId),parseInt(this.CreatorId),this.categoryMap[this.Category],this.duration,this.title,this.difficultyMap[this.difficulty],this.NoOfQuestions)
                .then(response =>{
                    toast.success("Edit succesfull for quiz",{
                        autoClose:4000
                    })
                    this.quizId = '',
                    this.CreatorId ='',
                    this.Category ='',
                    this.duration = '',
                    this.title = '',
                    this.difficulty = '',
                    this.NoOfQuestions = '',
                    this.showSearchResults = false
                    console.log(response)
                })
                .catch(()=>{
                     toast.error("Could not update the quiz one or more field is missing",{
                        autoClose:4000
                     })
                })

            },
            
            selectCategory(event){
               console.log(event.target.value);
               this.selectedCategory = event.target.value
            },
            inputChange(event){
               let image = event.target.files[0]
               updateImage(sessionStorage.getItem('Name'),image)
               .then(response =>{
                 sessionStorage.setItem('Image',response.data.imagePath)
                 this.image = "http://localhost:5193"+response.data.imagePath;
                 console.log(this.image)
               })
               
             },
            addQuestion(){
                event.preventDefault();
                console.log(this.QuestionName,this.Category,this.Points)
                 const question = async()=>{
                    try {
                         await addQuestion(
                            this.QuestionName,
                            this.difficultyMap[this.difficulty],
                            this.categoryMap[this.Category],
                            this.Points
                        );
                        toast.success("Successfully added question", {
                            autoClose: 4000
                        });
                        this.QuestionName = '',
                        this.Category = '',
                        this.Points = ''

                    } catch (error) {
                        console.error("Error adding question:", error);
                        if(error.response.data.errors.QuestionName){
                            toast.error(error.response.data.errors.QuestionName, {
                            autoClose: 4000
                          });
                        }
                        toast.error("Could not add question", {
                            autoClose: 4000
                        });
                    }
                    }
                 question();
                
            },

            addLeaderBoard(){
                 event.preventDefault();
                 console.log(this.LeaderBoardName,this.categoryMap[this.Category],this.quizId)
                 addLeaderBoard(this.LeaderBoardName,this.categoryMap[this.Category],this.quizId)
                 .then(response => {
                    toast.success("Leaderboard added", {
                        autoClose:4000
                    })
                    this.LeaderBoardName ='',
                    this.Category = '',
                    this.quizId=''
                    console.log(response);
                 })
                 .catch(()=>{
                     toast.error("Could not create leader board",{
                        autoClose:4000
                     })
                 })               
                
            },

            createQuiz(){
            //    event.preventDefault();
               console.log(this.CreatorId,this.categoryMap[this.Category],this.duration,this.title,this.difficultyMap[this.difficulty],this.NoOfQuestions)
               addQuiz(parseInt(this.CreatorId),this.categoryMap[this.Category],this.duration,this.title,this.difficultyMap[this.difficulty],this.NoOfQuestions)
               .then( () => {
                 toast.success("Quiz created Succesfully",{
                    autoClose:4000
                 })
                  
                    this.CreatorId ='',
                    this.Category ='',
                    this.duration = '',
                    this.title = '',
                    this.difficulty = '',
                    this.NoOfQuestions = ''
                    this.showSearchResults = false
                })
                 .catch(()=>{
                    toast.error("Could not create quiz one or more fields are missing",{
                        autoClose:4000
                    })
                 })
               
            },
            correctSelection(event){
                console.log(this.map[event.target.value]);
                this.isCorrect = this.map[event.target.value];
            },
            addMoreOption(){
                event.preventDefault();
                try{
                    if(this.optionText && this.QuestionId ){
                        this.options.push({
                        isCorrect:this.isCorrect,                  
                        text:this.optionText,
                        questionId:this.QuestionId 
                    })

                    }
                    else{
                        toast.error("Options fields are empty, please fill and submit",{
                            autoClose:4000
                        })
                    }
                    console.log(this.options);
                    this.questionId = '',
                    this.optionText = '',
                    this.isCorrect = false
                }
                catch(error){
                    toast.error("Options is empty",{
                        autoClose:4000
                    })
                }
                
            },
            submitOption(){
                // event.preventDefault();
                console.log(this.options);
                this.addMoreOption();
                 const question = async()=>{
                    try{
                        await addOptions(this.options);
                        toast.success("Options added succesfully",{
                            autoClose:4000
                        })
                        this.options = []
                    }
                    catch(error){
                        toast.error("Could not add options one or more field is empty",{
                            autoClose:4000
                        })
                    }
                    
                 }

                 question();
            },
            searchQuestions(){
                event.preventDefault();
                console.log(this.searchQuestion);
                const question = async()=>{
                     console.log(this.searchQuestion)
                     let question = await searchQuestions(this.searchQuestion);
                     this.searchResults = question.data;
                     this.showSearchResults = true;
                     console.log(question);

                }
                question()
            },
            searchQuestionsDelete(){
                event.preventDefault();
                console.log(this.searchQuestion);
                const question = async()=>{
                     console.log(this.searchQuestion)
                     let question = await searchQuestions(this.searchQuestion);
                     this.showSearchResults = true;
                     this.searchResults = question.data;
                     console.log(question);

                }
                question()
            },
            onclickDifficulty(event){
                this.difficulty = event.target.value
            },
            onclick(event){
                console.log(event.target.value)
                this.Category = event.target.value
            },
            onSelect(event){
               event.preventDefault()
               console.log(event.target.value)
               this.QuestionId = event.target.value
               let result = this.searchResults.find((question)=>{
                   return question.questionId == this.QuestionId
               })
               console.log(result);
               this.QuestionName = result.questionName
               
            },
            onSelectDelete(event){
                event.preventDefault();
                console.log(event.target.value)
                this.QuestionId = event.target.value
            },
            deleteSelection(text){
                event.preventDefault();
                this.options = this.options.filter((option)=>{
                    return option.text !== text;               
                })
            },
            searchQuizByTitle(){
                event.preventDefault();
                getAllQuiz(this.searchQuiz)
                .then(response => {
                    this.searchResultQuiz  = response.data
                    this.showSearchResults = true
                    console.log(response);
                })
                .catch(()=>{
                     toast.error("Some fields are empty please add try submitting ", {
                        autoClose:4000
                     })
                  })
            },
            deleteScoreCard(){
                 event.preventDefault();
                  deleteScoreCard(this.ScoreCardId)
                  .then((response) =>{
                     toast.success(response.data,{
                        autoClose:4000
                     })
                  })
                  .catch(()=>{
                     toast.error("Some fields are empty please add try submitting ", {
                        autoClose:4000
                     })
                  })
            },
            deleteOption(){
                 event.preventDefault();
                  deleteOption(this.optionId)
                  .then(() =>{
                     toast.success("Option deleted successully",{
                        autoClose:4000
                     })
                  })
                  .catch(()=>{
                     toast.error("Some fields are empty please add try submitting ", {
                        autoClose:4000
                     })
                  })
            },
            searchUserByName(){
                event.preventDefault();
                console.log("searching//")
                getUserByName(this.searchUser)
                .then(response =>{
                    this.searchResultUser = response.data
                    console.log(response.data)
                })

            },
            searchBoardByName(){
                event.preventDefault();
                console.log(this.searchBoard)
                getLeaderBoardByName(this.searchBoard)
                .then(response =>{
                    this.searchResultBoard = response.data
                    console.log(response.data)
                })
            },
            onSelectBoard(event){
                event.preventDefault();
                this.boardId = event.target.value
            },
            onSelectQuiz(event){
                event.preventDefault();
                this.quizId = event.target.value
                let result = this.searchResultQuiz.find((quiz)=>{
                    return quiz.quizId == this.quizId;
                })
                this.duration = result.duration
                this.title = result.title
                this.difficulty = result.difficulty
                this.NoOfQuestions = result.questions.length;
                console.log(result);

            },
            onSelectUser(event){
                event.preventDefault();
                this.CreatorId = event.target.value
            },
            deleteQuestion1(){
                event.preventDefault()
                deleteQuestion(this.QuestionId)
                .then(() =>{
                    toast.success("Succesfully deleted question",{
                        autoClose:4000
                    })
                    this.showSearchResults = false
                    this.QuestionId = ''
                })
                .catch(()=>{
                   toast.error("Could not delete question",{
                     autoClose:4000
                   })
                })
            },
            deleteQuiz(){
                event.preventDefault();
                deleteQuiz(this.quizId)
                .then(() =>{
                    toast.success("Succesfully deleted quiz",{
                        autoClose:4000
                    })
                })
                .catch(()=>{
                   toast.error("Could not delete quiz",{
                     autoClose:4000
                   })
                })
            },
            deleteBoard(){
                event.preventDefault()
                 deleteLeaderBoard(this.boardId)
                 .then(response=>{
                    console.log(response.data)
                 })
                 .catch(()=>{
                   toast.error("Could not delete leaderboard",{
                     autoClose:4000
                   })
                })
            },
            
            
    
        }
    },
    mounted(){
               fetch("http://localhost:5193/api/Quiz/Getallcategory")
                  .then( data => data.json())
                  .then( response =>{
                    this.Categories = response;
                    console.log("hello");
                  });
                  
                  this.image = "http://localhost:5193"+sessionStorage.getItem('Image')
                   this.username = sessionStorage.getItem('Name')
                   this.role = sessionStorage.getItem('Role')
                   console.log(this.image)
     },
     created(){
         this.image = sessionStorage.getItem('Image')
     }
}

</script>

<style scoped>
.search-result{
    display: flex;
    align-items: center;
    justify-content: space-between;
}
 .delete-div{
    border: 1px solid black;
    width: 80%;
    margin: auto;
    min-height: 80vh;
 }
 .selection-type{
   display: flex;
    margin: auto;
    width: 50%;
    flex-direction: column;
    color: white
 }
 .profile{
    position: absolute;
    top: 15%;
    width: 100%;
    display: flex;
    flex-direction: column;
    gap: 40px;
 }
 .category-selection{
    height: 40px;
    border-radius: 8px;
    border: none;
    text-align: center;
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
 .create-form{
    margin: auto;
    display: flex;
    gap: 20px;
    padding: 20px;
    background: #ebe6e6;
    box-sizing: border-box;
    width: 50%;
    box-shadow: 4px 8px 26px 4px;
    flex-direction: column;
    border-radius: 8px;
 }
 .create-div,
 .delete-div,
 .edit-div{
    box-shadow: 4px 16px 16px 2px;
    border: none;
    border-radius: 8px;
    width: 80%;
    gap: 16px;
    margin: auto;
    background: rgb(123, 63, 153);
    min-height: 80vh;
    display: flex;
    flex-direction: column;
    font-family: "Sour Gummy";
 }
 h4{
    font-size: xx-large;
    color: white;
    margin:32px auto 0px;
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
 .d-flex-8{
    display: flex;
    gap: 8px;
 }
 .w-100{
    width: 100%;
 }
 .svg-cam{
    width: 32px;
    position: relative;
    top: 64px;
    right: 32px;
 }
 .profile-desc{
    display: flex;
    flex-direction: column;
    justify-content: center;
 }

 @media screen and  (max-width:720px){
    .create-div,
     .delete-div,
    .edit-div{
     width: 90%;
   }
   .create-form{
    width: 90%;
   } 
   .selection-type{
    width: 90%;
   }
   .profile-holder{
    flex-direction: column-reverse;
    padding: 8px;
   }
   .profile-desc{
    width: 100%;
   }
   .profile-img{
    width: 100%;
   }
 }
</style>