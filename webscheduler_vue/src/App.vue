<template>
    <navbar></navbar>
    <div class="app">
      <router-view></router-view>
    </div>
</template>

<script>
import Navbar from "./components/UI/Navbar";
export default {
  components: {Navbar}
}
</script>

<style>
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
  font-family: 'M PLUS 1', sans-serif;
  font-weight: 600;
}

body::-webkit-scrollbar {
  /*width: 0;*/
}

.app{
  padding: 20px;
}


.observer{
  height: 30px;
}

.app__btns{
  margin: 15px 0;
  display: flex;
  justify-content:space-between;
}
</style>