<template>
 <div class="search-btn">
    <form class="form-inline my-2 my-lg-0">
      <input class="form-control mr-sm-2" type="search" placeholder="Search" aria-label="Search">
      <button class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
    </form>
</div>
<div class="card-holder">
  <div class="card" v-for="quiz in quizes" style="width: 18rem;" :key="quiz.id">
  <img class="card-img-top"  v-bind:src=imageMap[quiz.questions[0].category] alt="Card image cap">
  <div class="card-body">
    <h5 class="card-title">{{ quiz.title }}</h5>
    <section class="quiz-holder">
        <div class="card-text points-qestions">Max Points: {{ quiz.maxPoints }}</div >
        <div p class="card-text points-qestions">No of Questions: {{ quiz.questions.length }}</div >            
        <div class="card-text points-qestions">Category:{{ categoryMap[quiz.questions[0].category] }}</div>
        <div class="card-text points-qestions">Difficulty:{{ quiz.difficulty }}</div>     
    </section>
    <!-- <a href="#" class="btn btn-primary" @click="getQuiz()"> -->
        <!-- <router-link to="/takequiz">Take Quiz</router-link> -->
        <button @click="startQuiz(quiz.quizId)" type="button" class="btn btn-primary" >
            Start Quiz
        </button>

         
    <!-- </a> -->
  </div>
</div>
</div>

<div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">Modal title</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
        ...
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
        <button type="button"  class="btn btn-primary">Save changes</button>
      </div>
    </div>
  </div>
</div>
</template>
<script>
import { ref, onMounted } from 'vue'; // Importing ref and onMounted for Composition API
import { useRouter } from 'vue-router';
import { getAllQuiz } from '@/scripts/QuizService'; // Make sure the import path is correct

export default {
  name: 'QuizComponent',
  setup() {
    const router = useRouter();

    // Define the reactive state using ref
    const quizes = ref([]);
    const categoryMap = {
      "0": "GeneralKnowledge",
      "1": "Sports",
      "2": "Politics",
      "3": "Geography",
      "4": "History"
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

    // Fetch the quizzes when the component is mounted
    onMounted(() => {
      getAllQuiz()
        .then(response => {
          quizes.value = response.data;
          console.log(response.data);
        })
        .catch(error => {
          console.error('Error fetching quizzes:', error);
        });
    });

    // Return the state and methods to be used in the template
    return {
      quizes,
      categoryMap,
      imageMap,
      startQuiz
    };
  },
    created(){
        getAllQuiz()
        .then(response =>{
            this.quizes = response.data
            console.log(response.data);
        })
    }
 }

</script>

<style scoped>
   .card{
    margin: auto;
    box-shadow: 0px 12px 36px 4px;
    width: 18rem;
    transition: transform 0.3s ease, box-shadow 0.3s ease;
   }
   .card-holder{
    position: absolute;
    top: 20%;
    width: 100%;
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
   }
   .card:hover {
  transform: scale(1.15); /* Slightly enlarge the card */
  box-shadow: 0 8px 16px rgba(0, 0, 0, 0.3); /* Add a shadow on hover */
}
.card-body {
    font-family: "Sour Gummy", sans-serif;
  position: relative;
  overflow: hidden;
  background-color: #f8f9fa;
}

.card-body::before {
  content: '';
  position: absolute;
  top: 0;
  left: -100%;
  width: 200%;
  height: 100%;
  background: linear-gradient(120deg, rgba(255, 255, 255, 0) 0%, rgba(255, 255, 255, 0.4) 50%, rgba(255, 255, 255, 0) 100%);
  transform: skewX(-30deg);
  transition: all 0.8s ease;
  animation: shine 3s infinite linear;
}

@keyframes shine {
  0% {
    left: -100%;
  }
  100% {
    left: 100%;
  }
}


</style>