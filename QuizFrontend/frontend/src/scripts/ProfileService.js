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

export function addLeaderBoard(LeaderBoardName,Category,quizId){
  console.log("Leader from service")
  return axios.post("http://localhost:5193/api/LeaderBoard",{
     "leaderBoardName": LeaderBoardName,
     "category": Category,
     "quizId": quizId
  })
}

export function getAllQuiz(title){
   return axios.get(`http://localhost:5193/api/Quiz/search?quizTitle=${title}`)
}

export function getUserByName(name){
   return axios.get(`http://localhost:5193/User/search?name=${name}`)
}

export function getLeaderBoardByName(name){
  return axios.get(`http://localhost:5193/api/LeaderBoard/Search?name=${name}`)
}

export function deleteQuiz(id){
  return axios.delete(`http://localhost:5193/api/Quiz/${id}`)
}

export function deleteLeaderBoard(id){
  return axios.delete(`http://localhost:5193/api/LeaderBoard/${id}`)
}

export function deleteQuestion(id){
  return axios.delete(`http://localhost:5193/api/Question/${id}`)
}

export function deleteScoreCard(id){
  return axios.delete(`http://localhost:5193/api/ScoreCard/${id}`)
}

export function getAllScoreCard(){
  return axios.get("http://localhost:5193/api/ScoreCard");
}

export function updateImage(username,image){
  const formData = new FormData();
  formData.append("imageFile", image)
  return axios.post(`http://localhost:5193/User/upload-image/${username}`, formData,{
     headers:{
       "Content-Type":"multipart/form-data"
     }
   }
  );
}

export function updateQuiz(id){
  return axios.put(`http://localhost:5193/api/Quiz/${id}`)
}

export function updateLeaderBoard(id,LeaderBoardName,Category,quizId){
  return axios.put(`http://localhost:5193/api/LeaderBoard/${id}`,{
    "leaderBoardName": LeaderBoardName,
    "category": Category,
    "quizId": quizId
 })
}

export function updateQuestion(id,questionName,category,points){
  return axios.put(`http://localhost:5193/api/Question/${id}`, {
    "QuestionName": questionName,
    "Category": category,
    "Points":points
  })
}
