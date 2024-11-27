<template>
    <nav class="navbar navbar-expand-lg bg-body-tertiary">
  <div class="container-fluid">
    <a class="navbar-brand" href="#" style="font-family: Sour Gummy">
      <img width="60px" src="../assets/quiz2.png" />
      QuizTime
    </a>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarSupportedContent">
      <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item">
          <a class="nav-link" href="#"> 
          <router-link to="/">Home</router-link>
          </a>
        </li>
        <li class="nav-item">
          <a class="nav-link" href="#"> 
          <router-link to="/quiz">Quiz</router-link>
          </a>
        </li>

        <!-- <li class="nav-item" v-if="role== 1">
          <a class="nav-link" aria-disabled="true" >
            <router-link to="/profile">Creator Profile</router-link>
          </a>
        </li>

        <li class="nav-item" v-if="role== 0">
          <a class="nav-link" aria-disabled="true" >
            <router-link to="/userprofile">User Profile</router-link>
          </a>
        </li> -->

        <li class="nav-item">
          <a class="nav-link" aria-disabled="true" >
            <router-link to="/leaderboard">Board</router-link>
          </a>
        </li>
        
        <div>
        <li class="nav-item dropdown" v-if="isLoggedin">
          <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
            Dashboard
            <img width="52px" style="border-radius:50%"  :src="image">
          </a>
          <ul class="dropdown-menu">
            
            <li v-if="role== 0"><a class="dropdown-item" href="#"><router-link to="/profile">Creator Profile</router-link></a></li>
            <li v-if="role == 1"><a class="dropdown-item" href="#">  <router-link to="/userprofile">User Profile</router-link></a></li>
            <li v-if="isLoggedin"><a class="dropdown-item" href="#"><router-link to="/login" @click="logout">Logout</router-link></a></li>
            <!-- <li><hr class="dropdown-divider"></li> -->
          </ul>
        </li>
        <li class="nav-item" v-if="!isLoggedin">
          <a class="nav-link" aria-disabled="true">
            <router-link to="/login">Login</router-link>
          </a>
        </li>
      </div>
      </ul>
      <!-- <form class="d-flex" role="search">
        <input class="form-control me-2" type="search" placeholder="Search" aria-label="Search">
        <button class="btn btn-outline-success" type="submit">Search</button>
      </form> -->
      
    </div>
  </div>
 
</nav>
</template>

<script>
 export default{
    name:'NavBarComponent',
    data(){
      return{
        image:"http://localhost:5193"+sessionStorage.getItem('Image'),
        isPlaying: false,
        isLoggedin :false,
        role:1,
      }
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
    isLogged(){
          if(sessionStorage.getItem('Token')){
            this.isLoggedin = true;
            this.role = sessionStorage.getItem("Role")
          }
    },
     logout(){
      sessionStorage.removeItem('Token');
      sessionStorage.removeItem('Answers');
      sessionStorage.clear();
      this.isLoggedin = false
     }
  },
  created(){
    this.isLogged();
  }
 }
</script>
<style scoped>
 a{
  text-decoration: none;
  font-family: Sour Gummy;
 }
 .nav-item {
    border-radius: 20px;
    font-size: 24px;
    position: relative; /* Needed for positioning the pseudo-element */
    display: inline-block; /* Ensure it respects content width */
}
.logout{
  display: flex;
    width: 12%;
}

.nav-item::after {
    content: "";
    display: block;
    position: absolute;
    bottom: 0; 
    left: 0;
    height: 4px; 
    width: 0; 
    background-color: rgb(138, 163, 247);
    transition: width 0.3s ease;
}

.nav-item:hover::after {
    width: 100%;
}
 .navbar-nav{
    display: flex;
    justify-content: center;
    width: 100%;
    align-items: center;
    gap: 20px;
 }
</style>