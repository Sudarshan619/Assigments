<script setup>
import { login } from '@/scripts/LoginService';
import { useLoginStore } from '@/stores/loginStore';
import { ref } from 'vue';
import { useRouter } from 'vue-router'; 
import { toast } from 'vue3-toastify';
import 'vue3-toastify/dist/index.css';

const email = ref('');
const password = ref('');
const loginStore = useLoginStore();
const router = useRouter();

const logon = (event) => {
  event.preventDefault();
  login(email.value, password.value).then((response) => {
    console.log(response.data);
    sessionStorage.setItem('Token', response.data.token);
    sessionStorage.setItem('Role', response.data.role);
    sessionStorage.setItem('Name', response.data.username);
    sessionStorage.setItem('Image', response.data.image);
    sessionStorage.setItem('Id',response.data.id)
    loginStore.login(response.data.username);
    toast.success('Login Successful!', { autoClose: 3000 });
    router.push('/');
  }).catch(() => {
    toast.error('Invalid Credentials. Please try again.', { autoClose: 3000 });
  });
};
</script>

<template>
  <div class="login-page">
    <div class="image-section">
      <img src="../assets/quiz5.png" alt="Quiz App Image" class="login-image" />
    </div>
    <div class="login-form-section">
      <div class="login-card">
        <h3>Welcome Back</h3>
        <p>Please login to continue</p>
        <form @submit="logon" class="login-form">
          <div class="input-group">
            <label for="email">Email</label>
            <input class="form-control" type="text" id="email" v-model="email" placeholder="Enter your email" required />
          </div>
          <div class="input-group">
            <label for="password">Password</label>
            <input class="form-control" type="password" id="password" v-model="password" placeholder="Enter your password" required />
          </div>
          <button type="submit" class="btn-login">Login</button>
          <router-link class="register-link" to="/register">Don't have an account? Register</router-link>
        </form>
      </div>
    </div>
  </div>
</template>

<style scoped>
.login-page {
  display: flex;
  position: absolute;
  top: 10%;
  width: 100%;
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
  transition: 1s linear;
}

.login-image {
  width: 80%;
  max-width: 400px;
}

.login-form-section {
  flex: 1;
  display: flex;
  justify-content: center;
  align-items: center;
}

.login-card {
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

.login-form {
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

.btn-login {
  background: #4facfe;
  color: white;
  padding: 0.75rem;
  border: none;
  border-radius: 8px;
  font-size: 1rem;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.btn-login:hover {
  background: #00f2fe;
}

.register-link {
  margin-top: 1rem;
  font-size: 0.85rem;
  color: #4facfe;
  text-decoration: none;
}

.register-link:hover {
  text-decoration: underline;
}
</style>
