import axios from "axios";

export function getAllQuiz(){
   return axios.get("http://localhost:5193/api/Quiz")    
}