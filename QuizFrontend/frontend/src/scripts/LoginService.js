import axios from "axios";

export function login(username,password){
    return axios.post('http://localhost:5193/User/Login', {
        "username": username,
        "password": password
      });
}

export function register(username,password,email,role){
  return axios.post('http://localhost:5193/User', {
      "email": email,
      "username": username,
      "role":role,
      "password": password
    });
}