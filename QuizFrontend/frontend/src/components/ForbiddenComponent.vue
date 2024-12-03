<template>
    <div class="redirector">
 
     <img src="forbidden.avif" width="30%">
           <a class="nav-link" href="#"> 
            <router-link to="/">Home</router-link>
           </a>
    </div> 
 </template>
 <script>
 export default{
     name:'ForbiddenComponent'
 }
 </script>
 
 <style>
 .redirector{
     top: 10%;
 }
 </style>