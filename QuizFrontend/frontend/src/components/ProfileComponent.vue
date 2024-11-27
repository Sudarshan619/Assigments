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
                <section class="create-form w-100" v-if="searchResultsDelete.length>0">
                <h5>Available questions</h5>
                <div v-for="searchResult in searchResultsDelete" :key="searchResult.id" class="search-result">
                    <aside>
                        {{ searchResult.questionName }}
                    </aside>
                    <button @click="onSelectDelete" class="btn btn-primary" :value="searchResult.questionId">
                        Select
                    </button>
                </div>
               
            </section>
            <form class="d-flex-8">
              <input class="form-control mr-sm-2" type="search" placeholder="Search" aria-label="Search" v-model="searchQuestion">
              <button @click="searchQuestionsDelete" class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
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
                <input class="form-control" type="number" id="QuestionId" v-model="QuestionId">
               </div>
               <button class="btn btn-success" type="submit">Delete Option</button>
            </form>
            <!-- Delete leaderboard -->
            <form class="create-form" v-if="this.selectedCategory === 'leaderboard'">
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
                <input class="form-control" type="number" id="QuestionId" v-model="QuestionId">
               </div>
               <button class="btn btn-success" type="submit">Delete ScoreCard</button>
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
                     <option value="option">Option</option>
                     <option value="question">Question</option>
                     <option value="leaderboard">LeaderBoard</option>
                     <option value="quiz">Quiz</option>
                     <option value="scorecard">ScoreCard</option>
                </select>               
            </div>
           
            <!-- Edit question -->
        <div v-if="this.selectedCategory === 'question'">
           <form class="create-form" >
              <input class="form-control mr-sm-2" type="search" placeholder="Search" aria-label="Search" v-model="searchQuestion">
              <button @click="searchQuestions()" class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
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
            <section v-else>
                No result found
            </section>
            <form class="create-form">
            <div>
                <label for="QuestionId">Question Id</label>
                <input class="form-control" type="number" id="QuestionId" v-model="QuestionId">
            </div>
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
            <div>
                <label for="Points">Score</label>
                <input class="form-control" type="number" id="Points" v-model="Points">
            </div>
            <button class="btn btn-success" @click="addQuestion" type="submit">Edit Question</button>
            </form>
        </div>

        <!-- Edit quiz -->
        <div>
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
                    <button @click="onSelectQuiz" class="btn btn-primary" :value="searchResult.id">
                        Select
                    </button>
                </div>
               
            </section>
            <div>
                <label for="QuestionName">Creator Id</label>
                <input class="form-control" type="text" id="QuestionName" v-model="CreatorId">
            </div>
            <div>
                <label for="QuestionName">Qtitle</label>
                <input class="form-control" type="text" id="QuestionName" v-model="QuestionName">
            </div>
            <div style="display: flex;flex-direction: column;">
                <label for="Category">Category</label>
                <select @change="onclick" class="category-selection">
                     <option style="display:none">Select option</option>
                     <option v-for="category in Categories" :key="category.id" :value="category">{{ category }}</option>
                </select>               
            </div>
            <div>
                <label for="Points">Difficulty</label>
                <input class="form-control" type="number" id="Points" v-model="Points">
            </div>
            <div>
                <label for="Points">No of Questions</label>
                <input class="form-control" type="number" id="Points" v-model="Points">
            </div>
            <button class="btn btn-warning" @click="addQuestion" type="submit">Edit Quiz</button>
            </form>
        </div>

        <!-- Edit leaderboard -->
        <div>
               <form class="create-form" v-if=" this.selectedCategory === 'leaderboard' ">
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
                    <button @click="onSelectQuiz" class="btn btn-primary" :value="searchResult.leaderBoardId">
                        Select
                    </button>
                </div>
               
            </section>
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
            <button class="btn btn-primary" @click="addLeaderBoard" type="submit">Update LeaderBoard</button>
            </form>
        </div>

         </div>
         <aside>

         </aside>
       </section>

      
       

       <!-- Create Option -->
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
            <div>
                <label for="Points">Score</label>
                <input class="form-control" type="number" id="Points" v-model="Points">
            </div>
            <button class="btn btn-success" @click="addQuestion" type="submit">Create Question</button>
            </form>

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
                    <button @click="onSelectQuiz" class="btn btn-primary" :value="searchResult.id">
                        Select
                    </button>
                </div>
               
            </section>
            <div>
                <label for="QuestionName">Creator Id</label>
                <input class="form-control" type="text" id="QuestionName" v-model="CreatorId">
            </div>
            <div>
                <label for="QuestionName">Qtitle</label>
                <input class="form-control" type="text" id="QuestionName" v-model="QuestionName">
            </div>
            <div style="display: flex;flex-direction: column;">
                <label for="Category">Category</label>
                <select @change="onclick" class="category-selection">
                     <option style="display:none">Select option</option>
                     <option v-for="category in Categories" :key="category.id" :value="category">{{ category }}</option>
                </select>               
            </div>
            <div>
                <label for="Points">Difficulty</label>
                <input class="form-control" type="number" id="Points" v-model="Points">
            </div>
            <div>
                <label for="Points">No of Questions</label>
                <input class="form-control" type="number" id="Points" v-model="Points">
            </div>
            <button class="btn btn-warning" @click="addQuestion" type="submit">Create Quiz</button>
            </form>

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
            <button class="btn btn-primary" @click="addLeaderBoard" type="submit">Create LeaderBoard</button>
            </form>
            
            <form class="create-form" v-if=" this.selectedCategory === 'option'">
            <form class="d-flex-8">
              <input class="form-control mr-sm-2" type="search" placeholder="Search" aria-label="Search" v-model="searchQuestion">
              <button @click="searchQuestionsDelete" class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
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
import { updateImage,addQuestion ,addOptions,getAllQuiz,getUserByName,getLeaderBoardByName,addLeaderBoard,deleteQuiz, deleteLeaderBoard, deleteQuestion} from '@/scripts/ProfileService';
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
            CreatorId:'',                    
            QuestionName:'',
            LeaderBoardName:'',
            Category:'',
            Points:'',  
            searchQuestion:'',
            searchQuiz:'',
            searchUser:'',
            searchBoard:'',
            isCorrect:'',
            optionText:'',
            quizId:'',
            boardId:'',
            Categories:[],
            searchResultsDelete:[],
            searchResultBoard:[],
            searchResultCreate:[],
            searchResultQuiz:[],
            searchResultUser:[],
            searchResults:[],
            options:[],
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
                    let data =  await addQuestion(this.QuestionName,this.categoryMap[this.Category],this.Points);
                    if(data.status == 200){
                        toast.success("Succesfully added question",{
                            autoClose:4000
                        })
                    }
                    else{
                        toast.error("Could not add question",{
                            autoClose:4000
                        })
                    }
                 }

                 question();
            },
            addLeaderBoard(){
                 event.preventDefault();
                 console.log(this.LeaderBoardName,this.categoryMap[this.Category],this.quizId)
                 addLeaderBoard(this.LeaderBoardName,this.categoryMap[this.Category],this.quizId)
                 .then(response => {
                    console.log(response.data)
                 })
                    
                
            },
            correctSelection(event){
                console.log(this.map[event.target.value]);
               this.isCorrect = this.map[event.target.value];
            },
            addMoreOption(){
                event.preventDefault();
                this.options.push({
                    isCorrect:this.isCorrect,                  
                    text:this.optionText,
                    questionId:this.QuestionId                  
                })
                console.log(this.options);
                this.questionId = '',
                this.optionText = '',
                this.isCorrect = false
            },
            submitOption(){
                event.preventDefault();
                console.log(this.options);
                this.addMoreOption();
                 const question = async()=>{
                    let data =  await addOptions(this.options);
                    console.log(data);
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
                     this.searchResultsDelete = question.data;
                     console.log(question);

                }
                question()
            },
            onclick(event){
                console.log(event.target.value)
                this.Category = event.target.value
            },
            onSelect(event){
               console.log(event.target.value)
               this.QuestionId = event.target.value
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
                    console.log(response);
                })
            },
            searchUserByName(){
                event.preventDefault();
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
            },
            deleteQuestion1(){
                event.preventDefault()
                deleteQuestion(this.QuestionId)
                .then(() =>{
                    toast.success("Succesfully deleted question",{
                        autoClose:4000
                    })
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
            }
            
    
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