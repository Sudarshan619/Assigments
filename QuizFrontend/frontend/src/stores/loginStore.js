import { defineStore } from "pinia";


export const useLoginStore = defineStore('login', {
    state:()=>({
        username:null,
        isLoggedIn:false,
    }),
    actions:{
        login(username){
            this.username = username;
            this.isLoggedIn = true;
        },
        logout(){
            this.username = null;
            this.isLoggedIn = false;
        }
    }
});