
<script>
// import {addQuestion} from '@/scripts/QuestionService'
import {getQuestions} from '@/scripts/QuestionService'
import QuestionComponent from './QuestionComponent.vue'

   export default {
      name:'QuestionsComponent',
      components:{
        QuestionComponent
      },
      data(){
          return {
              getAllQuestions:[],
            //   question:{
            //      "QuestionName": "",
            //      "Category": 0,
            //      "Points": 0
            //        },
            //   categoryMap :{
            //     "GeneralKnowledge":0,
            //     "Sports":1,
            //     "Politics":2,
            //     "Geography":3,
            //     "History":4
            //      },
            //   Category :[],
            //   addQuestion:()=>{
               
            //     event.preventDefault();
            //     console.log(this.QuestionName,this.Category,this.Points)
            //      const question = async()=>{
            //         let data =  await addQuestion(this.question.QuestionName,this.question.Category,this.question.Points);
            //         console.log(data);
            //      }

            //      question();
            //   },
            //   onClick(event){
            //     this.category = this.categoryMap[event.target.value];
            //      console.log(this.category)
            //   },
             
           }
      
          },
            //  mounted(){
            //    fetch("http://localhost:5193/api/Quiz/Getallcategory")
            //       .then( data => data.json())
            //       .then( response =>{
            //         this.Category = response;
            //         console.log("hello");
            //       }) 
            // },
            created(){
                getQuestions().then(response => {
                     this.getAllQuestions = response.data;
                     console.log(this.getAllQuestions);
                   });              
                
              }  
        }
</script>

<template>
   <div class="question">
        <!-- <form>
            <div>
                <label class="form-control" for="QuestionName">Question Text</label>
                <input class="form-control" type="text" id="QuestionName" v-model="question.QuestionName">
            </div>
            <div>
                <label class="form-control" for="Category">Category</label>
                <select @change="onClick">
                     <option v-for="category in Category" :key="category.id" :value="category">{{ category }}</option>
                </select>
                
            </div>
            <div>
                <label class="form-control" for="Points">Score</label>
                <input class="form-control" type="number" id="Points" v-model="question.Points">
            </div>
            <button class="btn btn-success" @click="addQuestion()" type="submit">Add Question</button>
        </form> -->
        <div v-for="question in getAllQuestions" :key="question.id">
           <QuestionComponent :question="question"/>
        </div>
    </div>

</template>

<style>

</style>