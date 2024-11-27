<script>
import { register } from '@/scripts/LoginService'
import router from '@/scripts/router';
import { toast } from 'vue3-toastify';
import 'vue3-toastify/dist/index.css';

export default {
  name: 'RegisterComponent',
  data() {
    return {
      email: '',
      username: '',
      password: '',
      role: '',
      rolesMap: {
        "QuizzCreator": 0,
        "QuizzTaker": 1
      },
      roles: [],
      register: (event) => {
        event.preventDefault();
        register(this.username, this.password, this.email, this.rolesMap[this.role])
          .then((response) => {
            toast.success("User registration succesfull",{
              autoClose:5000          
            })
            router.push("/login")
            console.log(response.data);
          })
          .catch((err) => {
            if(err.response.data.errors.Password)   {
              toast.error(err.response.data.errors.Password,{
              autoClose:5000
              })
            }
            if(err.response.data.errors.Username){
              toast.error(err.response.data.errors.Username,{
                autoClose:5000
              })
            }        
           
            console.log(err.response);
          })
      },
      onchange(event) {
        this.role = event.target.value;
      }
    }
  },
  mounted() {
    fetch("http://localhost:5193/User")
      .then(data => data.json())
      .then(response => {
        this.roles = response;
      })
  }
}
</script>

<template>
  <div class="register-page">
    <div class="image-section">
      <img src="../assets/quiz5.png" alt="Quiz App" class="register-image" />
    </div>
    <div class="register-form-section">
      <div class="register-card">
        <h3>Create an Account</h3>
        <p>Fill in the details to register</p>
        <form @submit="register" class="register-form">
          <div class="input-group">
            <label for="username">Username</label>
            <input class="form-control" type="text" id="username" v-model="username" placeholder="Enter your username" required />
          </div>
          <div class="input-group">
            <label for="email">Email</label>
            <input class="form-control" type="email" id="email" v-model="email" placeholder="Enter your email" required />
          </div>
          <div class="input-group">
            <label for="role">Role</label>
            <select class="form-control" @change="onchange" v-model="role" required>
              <option disabled value="">Select a role</option>
              <option v-for="role in roles" :key="role.id" :value="role">{{ role}}</option>
            </select>
          </div>
          <div class="input-group">
            <label for="password">Password</label>
            <input class="form-control" type="password" id="password" v-model="password" placeholder="Enter your password" required />
          </div>
          <button type="submit" class="btn-register">Register</button>
          <router-link class="login-link" to="/login">Already have an account? Login</router-link>
        </form>
      </div>
    </div>
  </div>
</template>

<style scoped>
.register-page {
  position: absolute;
  top: 10%;
  width: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  background: linear-gradient(135deg, #4facfe, #00f2fe);
  font-family: 'Poppins', sans-serif;
}

.image-section {
  flex: 1;
  display: flex;
  justify-content: center;
  align-items: center;
}

.register-image {
  width: 70%;
  max-width: 400px;
}

.register-form-section {
  flex: 1;
  display: flex;
  justify-content: center;
  align-items: center;
}

.register-card {
  background: white;
  padding: 2rem;
  border-radius: 15px;
  box-shadow: 0 10px 30px rgba(0, 0, 0, 0.1);
  text-align: center;
  width: 100%;
  max-width: 75%;
}

h3 {
  color: #333;
  font-size: 1.5rem;
  margin-bottom: 0.5rem;
}

p {
  color: #555;
  font-size: 0.9rem;
  margin-bottom: 1.5rem;
}

.register-form {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.input-group {
  text-align: left;
}

label {
  font-size: 0.85rem;
  color: #333;
  margin-bottom: 0.5rem;
  display: inline-block;
}

.form-control {
  width: 100%;
  padding: 0.75rem;
  border: 1px solid #ddd;
  border-radius: 8px;
  font-size: 0.9rem;
  outline: none;
  transition: border-color 0.3s ease;
}

.form-control:focus {
  border-color: #4facfe;
}

.btn-register {
  background: #4facfe;
  color: white;
  padding: 0.75rem;
  border: none;
  border-radius: 8px;
  font-size: 1rem;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.btn-register:hover {
  background: #00f2fe;
}

.login-link {
  margin-top: 1rem;
  font-size: 0.85rem;
  color: #4facfe;
  text-decoration: none;
}

.login-link:hover {
  text-decoration: underline;
}
</style>
