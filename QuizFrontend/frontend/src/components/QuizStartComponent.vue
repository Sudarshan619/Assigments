

<template>
 
 <header class="quiz-header">
<h1>Quiz start</h1>
 <section v-for="quiz in quizes.questions" class="quiz-section" :key="quiz.id"> 
    <aside class="question-text">
      {{ quiz.questionName }}
    </aside>
    <aside class="option-holder">    
            <label :for="option.optionId" class="option-input" v-for="option in quiz.options" v-bind:key="option.id" v-bind:style="{ display: quiz.answered ? 'none' : 'flex' }">
                <span>{{ option.text }}</span>
                <input @click="onclick" class="form-control" type="radio" :id="option.optionId" v-model="QuestionId" :value="option.optionId" >
            </label>         
    </aside>
    <div class="next-prev-div">
        <button class="btn btn-primary" @click="addQuestion" type="submit">Previous Question</button>
        <button class="btn btn-success" @click="submit" type="submit">Sumbit Answer</button>
        <button class="btn btn-primary" @click="addQuestion" type="submit">Next Question</button>     
    </div>
    
 </section>
</header>
</template>

<script>
import { getAllQuiz } from '@/scripts/QuizService';

export default {
    name:'QuizStartComponent',
    data(){
        return{
            quizes:[],
            currentOption:'',
            isAnswered:'',
            response:{
                userId:'1',
                quizId:'1',
                options:[]           

            },
            QuizId:'',
              onclick(event){
              console.log(event.target.value);
              this.currentOption = event.target.value;
              console.log(this.response.options)
            },
            submit(){
                  const exist = this.response.options.some((option)=>{
                  console.log()
                  option.optionId === this.currentOption;
                  console.log(this.isAnswered)
                }) 

                if(!exist){
                    this.isAnswered = true;
                    this.response.options.push({ optionId: this.currentOption });
                }
            }
        }
    },
    created(){
        getAllQuiz()
            .then(response =>{
                this.quizes = response.data[0];
                console.log(response.data[0])
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
</style>