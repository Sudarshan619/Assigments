<template>
  <h4>Hi {{custName}} My first component {{ msg }}</h4>
  <div v-html="divContent"></div>
  <div >{{ divContent }}</div>
  <div v-bind:Id="divId">This is div1</div>
  <div>{{ method() }}</div>
  <div v-if="check">Check is true</div>
  <ul>

    <li v-for="item in items" :key="item.id">
{{ item.id }}
{{ item.message }}
    </li>
  </ul>
</template>

<script>
export default{
    name :'FirstComponent',
    props :{
        msg :String
    },
    data(){
        return {
            custName : "Ramu",
            divContent: "<p style = 'color:red'>Div content</p>",
            divId:"div1",
            method(){
               return "this is function"
            },
            check:false,
            items:[{id:1,message:"hello"},{id:2,message:"world"},{id:3,message:"welcome"}]
        }
    }
  }
</script>

<style scoped>

  #div1{
    color: aqua;
  }

  h4{
    background-color: aqua;
  }
</style>