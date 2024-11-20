

<template>
 
 <header class="quiz-header">
<h1 style="text-align: center;">{{ quizes.title }}</h1>
 
 <div class="container">
	<section class="quiz-section" :key="currentQuestion.id" :id="currentQuestion.questionId"> 
    <aside class="question-text">
      {{currentQuestion.questionName }}
    </aside>
    <aside class="option-holder">    
            <label :for="option.optionId" class="option-input" v-for="option in currentQuestion.options" v-bind:key="option.id" >
                <span>{{ option.text }}</span>
                <input @click="onclick" class="form-control" type="radio" :id="option.optionId" v-model="QuestionId" :value="option.optionId" v-bind:style="{ background:isSelected?'green':'' }">
            </label>   
            <div>
               Submitted Option: {{}}
            </div>      
    </aside>
    
    <div class="next-prev-div">
        <button class="btn btn-warning" @click="submit(currentQuestion.questionId)" type="submit">Save and submit</button>
        <button v-if="currentPage == quizQuestions.length" class="btn btn-success" @click="submitQuiz" type="submit">Submit Quiz</button>
    </div>
    
 </section>
			
		</div>
		<div class="pagination">
  <button 
    class="page-btn page-step" 
    :class="{ active: currentPage === 1 }" 
    @click="changePage(1)">
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
    @click="changePage(quizQuestions.length)">
    »
  </button>
</div>
	
</header>
</template>

<script>
import { getQuiz,submitQuiz } from '@/scripts/QuizService';
// import {Paginate} from 'vuejs-paginate-next';
//Registration using vue is not supported in vue3 needs to be added in components

export default {
    name:'QuizStartComponent',

    data(){
        return{
            quizes:[],
            quizQuestions:[],
            currentPage:1,
            currentOption:'',
            currentQuestion:'',
            QuizId:'',
            response:{              
                quizId:1,
                userId:1,
                options:[]           
            },
        }
    },
    methods:{
            changePage(page) {
                this.currentPage = page;
                 this.currentQuestion = this.quizQuestions[page - 1]; // Update question
            },
            onclick(event){
              console.log(event.target.value);
              this.currentOption = event.target.value;
              console.log(this.response.options)
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
                    let existingResponseOption = this.response[0].options.find(
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
              event.preventDefault();
               submitQuiz(this.response)
               .then(data => {
                console.log(data)
               })
            }
          },
            
    created(){
        let quizId = this.$route.params.quizId;
        getQuiz(quizId)
            .then(response =>{
                this.quizes = response.data;
                this.quizQuestions = response.data.questions
                this.currentQuestion = this.quizQuestions[0];
                console.log(this.currentQuestion);
            })
         }  
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
  
  .next-prev-div{
    display: flex;
    justify-content: space-between;
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

.container:has(#page-1:target) > .vegetables > .list {
  transform: translateX(0%);
}
.container:has(#page-1:target) .page-step[data-shown="1"] {
  display: inline-flex;
}

.container:has(#page-2:target) > .vegetables > .list {
  transform: translateX(-100%);
}
.container:has(#page-2:target) .page-step[data-shown="2"] {
  display: inline-flex;
}

.container:has(#page-3:target) > .vegetables > .list {
  transform: translateX(-200%);
}
.container:has(#page-3:target) .page-step[data-shown="3"] {
  display: inline-flex;
}

.container:has(#page-4:target) > .vegetables > .list {
  transform: translateX(-300%);
}
.container:has(#page-4:target) .page-step[data-shown="4"] {
  display: inline-flex;
}

.container:has(#page-5:target) > .vegetables > .list {
  transform: translateX(-400%);
}
.container:has(#page-5:target) .page-step[data-shown="5"] {
  display: inline-flex;
}
</style>