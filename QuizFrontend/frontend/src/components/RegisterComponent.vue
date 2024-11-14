<script>
import {register} from '@/scripts/LoginService'


export default {
  name: 'RegisterComponent',
  data() {
    return {
      email: '',
      username:'',
      password: '',
      role:'',
      rolesMap:{
         "QuizzCreator":0,
         "QuizzTaker":1
      },
      roles:[],
      register:()=>{
        event.preventDefault();
        register(this.username,this.password,this.email,this.rolesMap[this.role])
        .then((response)=>{
            console.log(response.data);       
        },err=>{
            console.log(err);      
        })    
      },
      onchange(event){
        console.log(event.target.value)
        this.role = event.target.value
      } 
    } 
   },
   mounted(){
        fetch("http://localhost:5193/User")
        .then(data => data.json())
        .then(response =>{
            console.log(response);
            this.roles = response
        })
    }  
}   
</script>
<template>
  <div class="sign-in">
    <img  width="50%" style="margin: auto;" src="../assets/quiz.png" />
    <h3 style="color:purple;">Sign Up</h3>
    <div>
      <form  class="formdiv">     
        <label  for="email">Username</label>
        <input class="form-control" type="text" id="email" v-model="username">
        <label  for="email">Email</label>
        <input class="form-control" type="text" id="email" v-model="email">
        <label  for="email">Role</label>
        <select class="selection" @change="onchange">
            <!-- whenevr use eventlistern be carefull paramters if parentises is given it is waiting for parameter and event wil be undefines -->
            <option v-for="role in roles"  :value="role" :key="role.id">{{ role }}</option>
        </select>
        
        <label  for="password">Password</label>
        <input class="form-control" type="password" id="password" v-model="password">
      <button class="btn-login" @click="register()" type="submit">Register</button>
      <router-link to="/login">Login</router-link>
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
  .selection{
    border: none;
    width: -webkit-fill-available;
    height: 36px;
    border-radius: 8px;
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