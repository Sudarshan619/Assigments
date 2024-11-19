import axios from "./Interceptor";

export function addQuestion(questionName,category,points){
    return axios.post('http://localhost:5193/api/Question', {
        "QuestionName": questionName,
        "Category": category,
        "Points":points
      });  
}

export function addOptions(Options){
  console.log(Options)
  return axios.post("http://localhost:5193/api/Option/CreateMultipleOption",Options)
}