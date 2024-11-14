<script>
import {login} from '@/scripts/LoginService'


export default {
  name: 'LoginComponent',
  data() {
    return {
      email: '',
      password: '',
      login:()=>{
        event.preventDefault();
        login(this.email, this.password)
        .then((response)=>{
            console.log(response.data.username);  
            sessionStorage.setItem('Token',response.data.token);
            window.location.href= "/"       
        //   alert(response.data.username + ' is logged in');
        },err=>{
            console.log(err);      
        //   alert(err.response.data.errorMessage);
        })    
      },    
    }
  }
}   
</script>
<template>
  <div class="sign-in">
    <img  width="50%" style="margin: auto;" src="../assets/quiz.png" />
    <h3 style="color:purple;">Sign in</h3>
    <div>
      <form  class="formdiv">     
        <label  for="email">Username</label>
        <input class="form-control" type="text" id="email" v-model="email">
        <label  for="password">Password</label>
        <input class="form-control" type="password" id="password" v-model="password">
      <button class="btn-login" @click="login()" type="submit">Login</button>
      <router-link to="/register">Register</router-link>
    </form>
    </div>
  </div>
</template>
<style scoped>
  .formdiv{
    display: flex;
    flex-direction: column;
    gap: 12px;
    align-items: center;
    justify-content: flex-start;
    width: fit-content;
    margin: auto;
    
  }
  .btn-login{
    border-radius: 12px;
    background: blue;
    color: white;
    width: 50%;
    border: none;
    padding: 4px;
  }
  .sign-in
  {
    /* border: 1px solid; */
    width: 30%;
    border-radius: 20px;
    margin: 60px auto;
    font-size: larger;
    font-family: cursive;
    min-height: 50vh;
    display: flex;
    flex-direction: column;
    justify-content: space-around;
    box-shadow: 16px 16px 8px;
    padding: 20px;
    background: linear-gradient(90deg, rgba(255,255,255,1) 0%, rgba(0,99,255,0.6027661064425771) 62%);
    position: absolute;
    top: 10%;
    left: 38%;
  }
  
  body{
    background-color: deepskyblue !important;
  }

  </style>