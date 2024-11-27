import axios from "./Interceptor";

export function getQuizAll(){
   return axios.get("http://localhost:5193/api/Quiz")    
}

export function getQuiz(id){
   return axios.get(`http://localhost:5193/api/Quiz/${id}`)    
}

export function submitQuiz(response){
   console.log(response)
   return axios.post("http://localhost:5193/api/ScoreCard",response)
}

export function getByCategory(category){
   return axios.get(`http://localhost:5193/api/Quiz/by category?category=${category}`)
}