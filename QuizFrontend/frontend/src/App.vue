<template>
   <NavBarComponent></NavBarComponent>

   <router-view/>

</template>

<script>

// import LoginComponent from './components/LoginComponent.vue'
// import QuestionComponent from './components/QuestionComponent.vue'
import NavBarComponent from './components/NavBarComponent.vue'


export default {
  name: 'App',
  components: {
    // LoginComponent,
    // QuestionComponent,
    NavBarComponent
  },
  data(){
    return{
      audio:null
    };
  },
  mounted() {
    this.audio = new Audio("puzzle-game-bright-casual-video-game-music-249202.mp3"); // Load the audio file
    this.audio.loop = true; // Set loop if you want it to play continuously  
  },
  methods: {
    toggleMusic() {
      this.isPlaying = !this.isPlaying;
      if (this.isPlaying) {
        this.audio.play();
      } else {
        this.audio.pause();
      }
    },
  },
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  /* background: linear-gradient(135deg, #4facfe, #00f2fe) !important; */
}

@keyframes rotate {
  0% {
           transform: rotate(0deg);
     }
     100% {
           transform: rotate(360deg);
     }
 }
.out-top {
    animation: rotate 20s linear infinite;
    transform-origin: 13px 25px;
}
.in-top {
    animation: rotate 10s linear infinite;
    transform-origin: 13px 25px;
}
.out-bottom {
    animation: rotate 25s linear infinite;
    transform-origin: 84px 93px;
}
.in-bottom {
    animation: rotate 15s linear infinite;
    transform-origin: 84px 93px;
}
</style>

