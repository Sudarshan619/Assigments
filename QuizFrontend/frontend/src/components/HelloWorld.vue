<template>
  <div class="home-page">
    <header>
  <div class="header-content">
    <img src="../assets/simp1.png" alt="Quiz Header" class="header-image" />
    <div class="header-text">
      <h1>Welcome, {{ username || 'Guest' }}!</h1>
      <p>Challenge yourself, climb the leaderboard, and see how far your knowledge takes you!</p>
    </div>
  </div>
</header>

<section class="about-section">
  <img src="../assets/simp3.png" alt="About Us" class="section-image" />
  <div class="section-text">
    <h2>About Us</h2>
    <p>
      Welcome to our quiz platform, where learning meets fun! With a wide variety of quizzes across diverse topics, 
      there's something for everyone. Sharpen your knowledge, compete with others, and enjoy an engaging experience.
    </p>
  </div>
</section>

<section class="leaderboard-section">
  <img src="../assets/simp2.png" alt="Leaderboard" class="section-image" />
  <div class="section-text">
    <h2>Leaderboard</h2>
    <p>
      Think you’re the best? Prove it by climbing to the top of our leaderboard. Compete with friends and users from 
      around the world to showcase your knowledge. See your rank and celebrate your achievements.
    </p>
  </div>
</section>


<section class="quiz-section">
  <img src="../assets/quiz2.png" alt="Take a Quiz" class="section-image" />
  <div class="section-text">
    <h2>Take a Quiz</h2>
    <p>
      Dive into a world of quizzes designed to challenge your intellect and entertain you. Whether you love trivia, 
      general knowledge, or specialized topics, our quizzes are here to keep you engaged. Pick a quiz, and let the fun begin!
    </p>
    <router-link to="/quiz">
      <button>Start a Quiz</button>
    </router-link>
  </div>
</section>

  </div>
</template>

<script>
import { useLoginStore } from '@/stores/loginStore';
import { ref } from 'vue';

export default {
  name: 'HomePage',
  setup() {
    const loginStore = useLoginStore();
    const username = ref(loginStore.username);

    return { username };
  },
};
</script>

<style scoped>
.home-page {
  padding: 20px;
  text-align: center;
  font-family: 'Arial', sans-serif;
}

/* Header */
header {
  background-color: #f9f9f9;
  padding: 30px 20px;
  border-radius: 10px;
  margin-bottom: 30px;
}

.header-content {
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 20px;
}

.header-image {
  width: 30%;
  border-radius: 10px;
}

.header-text h1 {
  font-size: 2.5rem;
  color: #333;
  margin-bottom: 10px;
}

.header-text p {
  font-size: 1.2rem;
  color: #666;
}

/* Sections */
section {
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 20px;
  padding: 20px;
  background-color: #fff;
  border-radius: 10px;
  margin-bottom: 30px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
}

.section-image {
  width: 40%;
  border-radius: 10px;
}

.section-text {
  max-width: 50%;
  text-align: left;
}

.about-section {
  flex-direction: row-reverse; /* Reverse layout for visual interest */
}

section h2 {
  font-size: 2rem;
  color: #42b983;
}

section p {
  font-size: 1rem;
  color: #444;
}

/* Button */
button {
  background-color: #42b983;
  color: white;
  border: none;
  padding: 10px 20px;
  border-radius: 5px;
  cursor: pointer;
  font-size: 1rem;
  margin-top: 20px;
}

button:hover {
  background-color: #369970;
}
</style>
