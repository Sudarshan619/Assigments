

<template>
 
 <header class="quiz-header">
  <div class="report-holder">
     <h1 style="text-align: center;" class="title">{{ quizes.title }}</h1>
     <div>
       <button class="btn btn-warning report" >Report</button>
     </div> 
    </div>
 
 <div class="container">
	<section class="quiz-section" :key="currentQuestion.id" :id="currentQuestion.questionId"> 
    <TimerComponent v-if="duration > 0" :duration="duration"  @expired="handleTimeExpiry"  />
     
    <aside class="question-text">
      {{currentQuestion.questionName }}
    </aside>
    <aside class="option-holder">    
            <label :for="option.optionId" class="option-input" :class="{ active1: isContainsOption(option.optionId)}" v-for="option in currentQuestion.options" v-bind:key="option.id" >
                <span>{{ option.text }}</span>
                <input v-if="!isSelected(option.optionId)" @click="onclick" class="form-control" type="radio" :id="option.optionId"  v-model="QuestionId" :value="option.optionId" >
                <span v-if="isSelected(option.optionId)"
                  @click="onclick" class="form-control" type="radio" :id="option.optionId" :value="option.optionId" :class="{ selected: isSelected(option.optionId)}">
                  <svg  xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512"><path d="M438.6 105.4c12.5 12.5 12.5 32.8 0 45.3l-256 256c-12.5 12.5-32.8 12.5-45.3 0l-128-128c-12.5-12.5-12.5-32.8 0-45.3s32.8-12.5 45.3 0L160 338.7 393.4 105.4c12.5-12.5 32.8-12.5 45.3 0z"/></svg>
                </span>
            </label>        
    </aside>
    
    <div class="next-prev-div">
        <button class="btn btn-warning" @click="submit(currentQuestion.questionId)" type="submit">Save and submit</button>
        <button v-if="currentPage == quizQuestions.length" class="btn btn-success" type="submit" data-toggle="modal" data-target="#exampleModal">Submit Quiz</button>
    </div>
    
    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">Are you sure you want to submit the test</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body" v-if="quiz">
       
         <p></p>
      </div>
      <div class="modal-footer">
        <button type="button"  class="btn btn-danger" data-dismiss="modal">Go back</button>
        <button type="button" class="btn btn-primary" @click="submitQuiz"  data-dismiss="modal">Submit</button>
      </div>
    </div>
  </div>
</div>
 </section>
			
		</div>
		<div class="pagination">
  <button 
    class="page-btn page-step" 
    :class="{ active: currentPage === 1 }" 
    @click="changePage(currentPage-1)">
    «
  </button>
  <a 
    v-for="(question, index) in quizQuestions" 
    :key="question.id" 
    class="page-btn" 
    :class="{ active: currentPage === index + 1 }" 
    @click="changePage(index + 1)">
    {{ index + 1 }}
  </a>
  <button 
    class="page-btn page-step" 
    :class="{ active: currentPage === quizQuestions.length }" 
    @click="changePage(currentPage+1)">
    »
  </button>
</div>
	
</header>
</template>

<script>
import { getQuiz,submitQuiz } from '@/scripts/QuizService';
import TimerComponent from './TimerComponent.vue';
import { toast } from 'vue3-toastify';
import 'vue3-toastify/dist/index.css';
import router from '@/scripts/router';
// import localforage from 'localforage';
// import {Paginate} from 'vuejs-paginate-next';
//Registration using vue is not supported in vue3 needs to be added in components

export default {
    name:'QuizStartComponent',
    components:{TimerComponent},
    data(){
        return{
            quizes:[],
            quizQuestions:[],
            currentPage:1,
            currentOption:'',
            currentQuestion:'',
            duration:0,
            QuizId:'',
            response:{              
                quizId:6,
                userId:sessionStorage.getItem('Id'),
                options:[]           
            },
        }
    },
    methods:{
              initializeQuiz(quizData) {
                this.quizes = quizData;
                this.duration = quizData.duration;
                this.quizQuestions = quizData.questions;
                this.currentQuestion = this.quizQuestions[0];
                console.log(this.quizQuestions);
            },
            handleTimeExpiry(){
                toast("Your time is completed submitting the quix",{
                  autoClose:4000
                   }  
                )
                this.submitQuiz();
               
            },
            changePage(page) {
                 this.currentPage = page;
                 this.currentQuestion = this.quizQuestions[page - 1]; // Update question
            },
            onclick(event){
              console.log(event.target.value);
              this.currentOption = event.target.value;
              console.log(this.response.options)
            },
            isContainsOption(optionId){
              const storedAnswers = sessionStorage.getItem('Answers');
               if (!storedAnswers) return false; // Return false if no data is stored
         
               const answers = JSON.parse(storedAnswers) || [] ;
              //  if (!answers.options || !Array.isArray(answers.options)) {
              //   console.log(answers.options)
              //   console.log("false")
              //   return false
              // } 
         
               return answers.find((respOption) => respOption.optionId === optionId);
            },
            isSelected(optionId) {
                return this.currentOption === optionId.toString();
            },
            submit(questionId) {
                let answers = JSON.parse(sessionStorage.getItem('Answers') || '[]');
                let exist = answers.some((option) => option.questionId === questionId);
            
                if (!exist) {
                    let newOption = {
                        questionId: questionId,
                        optionId: parseInt(this.currentOption, 10),
                        optionName:""
                    };            
                    this.response.options.push(newOption); // Update response
                    answers.push(newOption); // Update session storage
                    sessionStorage.setItem('Answers', JSON.stringify(answers));
            
                    if (this.currentPage !== this.quizQuestions.length) {
                        this.changePage(this.currentPage + 1);
                    }
                } else {
                    let obj = answers.find((option) => option.questionId === questionId);
                    obj.optionId = parseInt(this.currentOption, 10); // Update existing option
            
                    // Reflect changes in response.Options
                    let existingResponseOption = this.response.options.find(
                        (opt) => opt.questionId === questionId
                    );
                    if (existingResponseOption) {
                        existingResponseOption.optionId = obj.optionId;
                    }
            
                    sessionStorage.setItem('Answers', JSON.stringify(answers));
                    
                    if (this.currentPage !== this.quizQuestions.length) {
                        this.changePage(this.currentPage + 1);
                    }
                }
            
                console.log("Updated Response:", this.response.options);
            },
            nextQuestion(event){
              console.log(event.target.dataset.questionId)
              const questionId = event.target.dataset.questionId;
              //filter returns an array
              //find returns an element --> use when single element is needed
               this.currentQuestion = this.quizQuestions.find((question)=>{
                  return question.questionId.toString() === questionId.toString()              
                })
              console.log(this.currentQuestion[0]);
            },
            submitQuiz(){
              console.log(JSON.parse(sessionStorage.getItem('Answers')))
              this.response.options = JSON.parse(sessionStorage.getItem('Answers') || [])
               submitQuiz(this.response)
               .then(data => {
                console.log(data);
                toast(`${data.data} ,do not refresh automatically redirecting to home page`, {
                    autoClose: 4000,
                 });
                 setTimeout(() => {
                  router.push("/")
                 }, 4000);
                 
               })
               sessionStorage.removeItem('Answers')
            }
          },
          created() {
    let quizId = this.$route.params.quizId;
    this.response.quizId = quizId;

    const cachedQuiz = sessionStorage.getItem(`quiz_${quizId}`);
    
    if (cachedQuiz) {
        // Parse and use the cached data
        const quizData = JSON.parse(cachedQuiz);
        this.initializeQuiz(quizData);
    } else {
        getQuiz(quizId)
            .then(response => {
                const quizData = response.data;
                this.initializeQuiz(quizData);
                sessionStorage.setItem(`quiz_${quizId}`, JSON.stringify(quizData));
            })
            .catch(error => {
                console.error("Error fetching quiz:", error);
            });
    }
},

}
  
</script>

<style scoped>
  .quiz-header{
    position: absolute;
    top: 15%;
    border-radius: 12px;
    width: 80%;
    margin: auto;
    margin:auto 120px;
    background: #65b3f8;
    padding: 20px;
  }
  .form-control{
    position: relative;
  }
  .selected{
    background-color: green;
  }
  .active1{
    background-color: rgb(171, 229, 171) !important;
  }
  
  .next-prev-div{
    display: flex;
    justify-content: space-between;
  }
  svg{
    width: 16px;
    height: 16px;
    position: absolute;
    left: 4px;
    top: 4px;
    fill: white;
  }
  .quiz-section{
    width: 80%;
    margin: auto;
    display: flex;
    flex-direction: column;
    gap: 8px;
    min-height: 50vh;
  }
  .page-btn.active {
  color: #ffffff;
  background-color: #1b95ff;
}
  .question-text{
    background: beige;
    min-height:10vh ;
    border-radius: 8px;
    display: flex;
    align-items: center;
    justify-content: center;
  }
  .option-input{
    width: 49%;
    height: 60px;
    border-radius: 8px;
    background-color: white;
    display: flex;
    align-items: center;
    padding: 16px;
  }
  span{
    width: 100%;
  }
  .form-control{
    width: 6%;
    border: 1px solid;
    height: 24px;
    border-radius: 50%;
  }
  .option-holder{
    display: flex;
    flex-wrap: wrap;
    gap: 8px;
    justify-content: center;
  }
  .container{
    font-family: "Baloo 2", sans-serif;
    font-weight: 700;
  }
  h1{
    font-family: Sour Gummy;
  }
  @import url("https://fonts.googleapis.com/css2?family=Gochi+Hand&family=Noto+Color+Emoji&display=swap");
* {
  box-sizing: border-box;
}

html {
  font-size: 18px;
}
.container > h1 {
  font-size: 2rem;
  text-align: center;
  padding: 1rem;
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

.page-num {
  display: none;
}

.page-step {
  display: none;
}
.report-holder{
  display: flex;
  width: 80%;
  align-items: center;
  margin: auto
}

.title{
  width: 100%;
}

@media screen and (max-width: 760px) {
  .quiz-header{
    width: 100%;
    margin: 0
  }
}

@media screen and (max-width: 560px) {
  .option-input{
    width: 100%;
  }
  .quiz-section{
    width: 100%;
  }
  
}

@media  screen and (max-width:380px) {
  .next-prev-div{
    flex-direction: column;
    gap: 8px;
  }
}


</style>