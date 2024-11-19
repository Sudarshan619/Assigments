<template>
    <header class="profile">
      <!-- Profile details -->
       <section class="profile-holder">  
            <aside class="profile-desc">
                 left 
            </aside>
            <aside class="profile-img">
                <img style="border-radius: 50%;" width="50%" height="auto" src="user.jpg" />
            </aside>     
       </section>
       
       <!-- Delete question -->
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
            <section class="create-form" v-if="searchResultsDelete.length>0">
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
            <form class="create-form">
              <input class="form-control mr-sm-2" type="search" placeholder="Search" aria-label="Search" v-model="searchQuestion">
              <button @click="searchQuestionsDelete" class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
            </form>
            <form class="create-form" v-if="this.selectedCategory === 'question'">
               <div>
                <label for="QuestionId">Question Id</label>
                <input class="form-control" type="number" id="QuestionId" v-model="QuestionId">
               </div>
               <button class="btn btn-success" type="submit">Delete Question</button>
            </form>
            <form class="create-form" v-if="this.selectedCategory === 'option'">
               <div>
                <label for="QuestionId">Option Id</label>
                <input class="form-control" type="number" id="QuestionId" v-model="QuestionId">
               </div>
               <button class="btn btn-success" type="submit">Delete Option</button>
            </form>
            <form class="create-form" v-if="this.selectedCategory === 'leaderboard'">
               <div>
                <label for="QuestionId">LeaderBoard Id</label>
                <input class="form-control" type="number" id="QuestionId" v-model="QuestionId">
               </div>
               <button class="btn btn-success" type="submit">Delete LeaderBoard</button>
            </form>
         </div>
         <aside>

         </aside>
          
       </section>

       <!-- Edit question -->
       <section>
        <div class="edit-div">
            <h4>Edit section</h4>
            <div class="selection-type">
                <label for="Category">Category</label>
                <select @change="onclick" class="category-selection">
                    <option style="display:none">Select option</option>
                     <option value="option">Option</option>
                     <option value="question">Question</option>
                     <option value="leaderboard">LeaderBoard</option>
                     <option value="quiz">Quiz</option>
                     <option value="scorecard">ScoreCard</option>
                </select>               
            </div>
           
            <form class="create-form">
              <input class="form-control mr-sm-2" type="search" placeholder="Search" aria-label="Search" v-model="searchQuestion">
              <button @click="searchQuestions()" class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
           </form>

           <section class="create-form" v-if="searchResults.length>0">
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
            <div>
                <label for="QuestionName">QTitle</label>
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
            <div>
                <label for="QuestionName">LeaderBoard Title</label>
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
                <label for="Points">Quiz Id</label>
                <input class="form-control" type="number" id="Points" v-model="Points">
            </div>
            <button class="btn btn-primary" @click="addQuestion" type="submit">Create LeaderBoard</button>
            </form>
            
            <form class="create-form" v-if=" this.selectedCategory === 'option'">
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
import { addQuestion ,addOptions} from '@/scripts/ProfileService';
import { searchQuestions } from '@/scripts/QuestionService';

export default{
    name:'ProfileComponent',
    data(){
        return{
            map:{
              "true": true,
              "false":false
            },
            selectedCategory:'question',
            QuestionId:'',
            QuestionName:'',
            searchQuestion:'',
            isCorrect:'',
            optionText:'',
            Category:'',
            Points:'',
            Categories:[],
            searchResultsDelete:[],
            searchResultCreate:[],
            searchResults:[],
            options:[],
            selectCategory(event){
               console.log(event.target.value);
               this.selectedCategory = event.target.value
            },
            addQuestion(){
                event.preventDefault();
                console.log(this.QuestionName,this.Category,this.Points)
                 const question = async()=>{
                    let data =  await addQuestion(this.question.QuestionName,this.question.Category,this.question.Points);
                    console.log(data);
                 }

                 question();
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
                console.log(event.target.value)
                this.QuestionId = event.target.value
            },
            deleteSelection(text){
                event.preventDefault();
                this.options = this.options.filter((option)=>{
                    return option.text !== text;               
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
                  }) 
     },
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
</style>