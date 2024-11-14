
<script>


export default {
    name: 'QuestionComponent',
    props: {
        question: {
          questionName: '',
          category:'',
          points:'',
        }
      }
  }
</script>
<template>
    <div>
      <h1>Question</h1>
      <div>{{ question.questionName }}</div>
    </div>
</template>
<style>
  
  </style>