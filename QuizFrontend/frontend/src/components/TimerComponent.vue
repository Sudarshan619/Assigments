<template>
    <div class="time-holder">
      <div class="timer">
        <svg xmlns="http://www.w3.org/2000/svg" width="20px" viewBox="0 0 448 512"><path d="M176 0c-17.7 0-32 14.3-32 32s14.3 32 32 32l16 0 0 34.4C92.3 113.8 16 200 16 304c0 114.9 93.1 208 208 208s208-93.1 208-208c0-41.8-12.3-80.7-33.5-113.2l24.1-24.1c12.5-12.5 12.5-32.8 0-45.3s-32.8-12.5-45.3 0L355.7 143c-28.1-23-62.2-38.8-99.7-44.6L256 64l16 0c17.7 0 32-14.3 32-32s-14.3-32-32-32L224 0 176 0zm72 192l0 128c0 13.3-10.7 24-24 24s-24-10.7-24-24l0-128c0-13.3 10.7-24 24-24s24 10.7 24 24z"/></svg>
        Time left:
        <span>{{ timer.days }}</span>:<span>{{ timer.hours }}</span>:
        <span>{{ timer.minutes }}</span>:<span>{{ timer.seconds }}</span>
      </div>
    </div>
  </template>
  
  <script>
  import { watchEffect, onMounted } from "vue";
  import { useTimer } from "vue-timer-hook";
  
  export default {
    name: "TimerComponent",
    props: {
      duration: {
        type: Number,
        required: true,
      },
    },
    emits: ["expired"],
    setup(props, { emit }) {
      const timerEndKey = "timerEndTime";
      const now = new Date();
      let endTime;
  
      const savedEndTime = sessionStorage.getItem(timerEndKey);
      if (savedEndTime) {
        endTime = new Date(savedEndTime);
        if (endTime < now) {
          sessionStorage.removeItem(timerEndKey);
          endTime = new Date(now.getTime() + props.duration * 60 * 1000);
          sessionStorage.setItem(timerEndKey, endTime.toISOString());
        }
      } else {
        endTime = new Date(now.getTime() + props.duration * 60 * 1000);
        sessionStorage.setItem(timerEndKey, endTime.toISOString());
      }

      const timer = useTimer(endTime);
      let isExpired = false;

      onMounted(() => {
        watchEffect(() => {
          if (timer.isExpired.value && !isExpired) {
            console.warn("Timer expired");
            emit("expired");
            sessionStorage.removeItem(timerEndKey);
            isExpired = true
          }
        });
      });
  
      return {
        timer,
      };
    },
  };
  </script>
  
  <style scoped>
  .timer {
    background-color: red;
    color: white;
    height: 40px;
    display: flex;
    border-radius: 8px;
    width: 25%;
    align-items: center;
    justify-content: center;
  }
   @media screen and (max-width:560px){
     .time-holder{
      width: 100%;
     }
     .timer{
    width: 100%;
     }  
   }
  </style>
  