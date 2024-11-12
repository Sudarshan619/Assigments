<template>
<h4>Products</h4>
<button @click="btnClick()" class="btn btn-success">Click here</button>
<select @change="onChangeDrop" style="height: 40px;border-radius: 4px;">
    <option >Select options</option>
    <option v-for="category in categories" :key="category.id" :value="category">{{ category }}</option>
</select>

<div v-if="products.length>0" class="product-list">
    <div v-for="product  in products" :key="product.id" class="card" style="width: 18rem;">
  <img v-bind:src= product.thumbnail class="card-img-top" alt="...">
  <div class="card-body">
    <h5 class="card-title">{{ product.title }}</h5>
    <p class="card-text">{{ product.description }}</p>
    <a href="#" class="btn btn-success">${{ product.price }}</a>
  </div>
</div>
    
</div>
</template>

<script>
  export default{
     name:'ProductList',
     data(){
       return{
            products:[],
            categories:[],
            btnClick:()=>{
                
            },

            onChangeDrop(element){
                console.log(element,"hello")
                fetch(`https://dummyjson.com/products/category/${element.target.value}`)
.then(res => res.json())
.then(data => {console.log(data.products);
this.products = data.products;});
            }
            
        }
     },
     mounted(){
        fetch('https://dummyjson.com/products')
                .then(res => res.json())
                .then(data =>{
                    console.log(data.products);
                    this.products = data.products;
                });
                fetch('https://dummyjson.com/products/category-list')
.then(res => res.json())
.then(data=>{
    console.log(data);
    this.categories = data})
        
     }

     
  }
</script>

<style>
  .product-list{
    display: flex;
    flex-wrap: wrap;
    gap: 8px;
  }
</style>