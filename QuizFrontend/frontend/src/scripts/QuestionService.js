import axios from "axios";

export function addQuestion(questionName,category,points){
    return axios.post('http://localhost:5193/api/Question', {
        "QuestionName": questionName,
        "Category": category,
        "Points":points
      });  
}

export function getQuestions(){
   return axios.get("http://localhost:5193/api/Question?pageNumber=1")    
}

export function searchQuestions(questionName){
  return axios.get(`http://localhost:5193/api/Question/Search?question=${questionName}`)    
}

