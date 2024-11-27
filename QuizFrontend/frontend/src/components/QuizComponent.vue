<template>
  <div class="quiz-holder-main">
 <div class="search-btn">
   <form class="d-flex-8">
        <input class="form-control mr-sm-2" type="search" placeholder="Search" aria-label="Search" v-model="searchQuiz">
        <button @click="searchQuizByTitle" class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
    </form>
    <select @change="getQuizByCategory" class="selector">
          <option value="0">General knowledge</option>
          <option value="1">Sports</option>
          <option value="2">Politics</option>
          <option value="3">Geography</option>
          <option value="4">History</option>
        </select>
</div>
<div class="card-holder" v-if="quizes.length == 0">
  <img src="../assets/notfound1.gif" class="notfound"/>
</div>
<div class="card-holder" v-if="quizes.length>0">
  <div class="card" v-for="quiz in quizes" style="width: 18rem;" :key="quiz.id">
  <img class="card-img-top"  v-bind:src=imageMap[quiz.questions[0].category] alt="Card image cap">
  <div class="card-body" :style="{background: difficulty[quiz.questions[0].difficulty]}">
    <h5 class="card-title">{{ quiz.title }}</h5>
    <section class="quiz-holder">
        <div class="card-text points-qestions">Max Points: {{ quiz.maxPoints }}</div >
        <div p class="card-text points-qestions">No of Questions: {{ quiz.questions.length }}</div >            
        <div class="card-text points-qestions">Category:{{ categoryMap[quiz.questions[0].category] }}</div>
        <div class="card-text points-qestions">Difficulty:{{ quiz.difficulty }}</div>     
    </section>
    <!-- <a href="#" class="btn btn-primary" @click="getQuiz()"> -->
        <!-- <router-link to="/takequiz">Take Quiz</router-link> -->
        <button @click="assignQuizId" type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal" :value="quiz.quizId">
            Start Quiz
        </button>
        
        <!-- @click="startQuiz(quiz.quizId)" -->
         
    <!-- </a> -->
  </div>
</div>
</div>

<div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">Are you ready to take the quiz</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body" v-if="quiz">
         <p>Quiz Timing:{{ quiz.duration }}</p>
         <p>Quiz Category:{{ quiz.questions[0].category }}</p>
         <p></p>
      </div>
      <div class="modal-footer">
        <button type="button"  class="btn btn-danger" data-dismiss="modal">Cancel</button>
        <button type="button" @click="startQuiz(quizId)"  class="btn btn-primary" data-dismiss="modal">Get Started</button>
      </div>
    </div>
  </div>
</div>
</div>
</template>
<script>
import { ref, onMounted } from 'vue'; // Importing ref and onMounted for Composition API
import { useRouter } from 'vue-router';
import { getQuizAll,getByCategory } from '@/scripts/QuizService'; // Make sure the import path is correct
import { getAllQuiz } from '@/scripts/ProfileService';
import { toast } from 'vue3-toastify';
import 'vue3-toastify/dist/index.css';

export default {
  name: 'QuizComponent',
  setup() {
    const router = useRouter();

    // Define the reactive state using ref
    const quizes = ref([]);
    const quiz = ref(null);
    const quizId = ref('');
    const searchQuiz = ref('');
    const categoryMap = {
      "0": "GeneralKnowledge",
      "1": "Sports",
      "2": "Politics",
      "3": "Geography",
      "4": "History"
    };
    const difficulty ={
       "0": "#a1faa1",
       "1": "orange",
       "2": "red"
    };
    const imageMap = {
      0: "gk.jpg",
      1: "political.jpg",
      2: "political.jpg",
      3: "geography.jpg",
      4: "history.jpg"
    };

    // Define the method to start the quiz
    const startQuiz = (quizId) => {
      router.push({ name: 'takequiz', params: { quizId } });
    };
    
    const assignQuizId = (event) =>{
       console.log(event.target.value)
       
       quizId.value =  event.target.value
       const answer = quizes.value.find((q)=>{
         return q.quizId == quizId.value
       })
       quiz.value = answer
       console.log(quiz.value)
    }

   const searchQuizByTitle = ()=>{
                event.preventDefault();
                console.log(searchQuiz.value)
                if(searchQuiz.value.length == 0){
                  getQuizes();
                }
                else{
                  getAllQuiz(searchQuiz.value)
                  .then(response => {
                    toast.success("Succesfully fetched the result",{
                      autoClose:3000
                     })
                      quizes.value  = response.data
                      console.log(response);
                   })
                   .catch(err =>{
                    toast.error("No quiz found for the search",{
                      autoClose:3000
                    })
                     console.log(err)
                   })
                }
              
    }

    const getQuizByCategory =(event)=>{
       
       getByCategory(event.target.value)
       .then(response => {
          quizes.value = response.data
          console.log(response.data)
       })
    }

    const getQuizes = ()=> getQuizAll()
        .then(response => {
          quizes.value = response.data;
          console.log(response.data);
        })
        .catch(error => {
          console.error('Error fetching quizzes:', error);
        });

    // Fetch the quizzes when the component is mounted
    onMounted(() => {
        getQuizes();
    });

    // Return the state and methods to be used in the template
    return {
      quizes,
      quiz,
      quizId,
      categoryMap,
      imageMap,
      searchQuiz,
      startQuiz,
      assignQuizId,
      searchQuizByTitle,
      getQuizes,
      difficulty,
      getQuizByCategory
    };
  },
 }

</script>

<style scoped>
   .card{
    margin: auto;
    box-shadow: 0px 12px 36px 4px;
    width: 18rem;
    transition: transform 0.3s ease, box-shadow 0.3s ease;
   }
   .d-flex-8{
    display: flex;
    gap: 8px;
   }
   .notfound{
    mix-blend-mode: hard-light;
   }
   .selector{
     border-radius: 4px;
     border: 1px solid gray;
   }

   .card-body{
    display: flex;
    flex-direction: column;
    align-items: center;
   }
   .card-holder{
    width: 80%;
    margin: auto;
    gap: 16px;
    flex-wrap: wrap;
    display: flex;
    justify-content: center;
   }
   .points-qestions{
    border: 2px solid white;
    display: flex;
    height: 40px;
    border-radius: 8px;
    align-items: center;
    justify-content: center;
    background: darkorchid;
    color: white;
   }
   .quiz-holder{
    display: flex;
    flex-direction: column;
    gap: 8px;
   }
   .form-inline{
    width: 30%;
    display: flex;
    justify-content: center;
    gap: 18px;
   }
   .search-btn{
    display: flex;
    justify-content: center;
    margin: 20px;
    gap:20px
   }
   .card:hover {
  transform: scale(1.15); 
  box-shadow: 0 8px 16px rgba(0, 0, 0, 0.3); /* Add a shadow on hover */
}
.card-body {
    font-family: "Sour Gummy", sans-serif;
  position: relative;
  overflow: hidden;
  background-color: #f8f9fa;
}

.card-body::before {
  /* content: '';
  position: absolute;
  top: 0;
  left: -100%;
  width: 200%;
  height: 100%;
  background: linear-gradient(120deg, rgba(255, 255, 255, 0) 0%, rgba(255, 255, 255, 0.4) 50%, rgba(255, 255, 255, 0) 100%);
  transform: skewX(-30deg);
  transition: all 0.8s ease; 
  animation: shine 3s infinite linear; */
}

@keyframes shine {
  0% {
    left: -100%;
  }
  100% {
    left: 100%;
  }
}

@media screen and (max-width:560px) {
  .search-btn{
    flex-direction: column;
    width: 60%;
    margin: 20px auto;
  }
  .selector{
    height: 40px;
  }
}

@media screen and (max-width:420px) {
  .search-btn{
    width: 80%;
  }
  
}

</style>