<template>
    <navbar></navbar>
    <div class="app">
      <router-view></router-view>
    </div>
</template>
<script>

export default {
  mounted() {
    if(JSON.parse(localStorage.getItem('isAuth'))){
      this.$store.commit('setTokens', {
        access: localStorage.getItem('accessToken'),
        refresh: localStorage.getItem('refreshToken')
      })
      this.$store.commit('setAuth', JSON.parse(localStorage.getItem('isAuth')))
      this.$store.commit('setAdmin', JSON.parse(localStorage.getItem('isAdmin')))
    }
  },
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

.form{
  display: flex;
  flex-direction: column;
}

.input {
  border: 1px solid #0c20a1;
  padding: 10px 15px;
  margin-top: 10px;
  margin-bottom: 10px;
}

.user__page{
  display: flex;
  justify-content: center;
}

.user__form{
  width: 400px;
}

.observer{
  height: 30px;
}

.app__btns{
  margin: 15px 0;
  display: flex;
  justify-content:space-between;

}

.page{
  margin: auto;
  width: 70vw;
}

.error{
  color: rgba(255, 0, 0, 0.94);
  font-size: 14px;
}

.link{
  text-decoration: none;
}
.link:hover{
  text-decoration: underline;
}
</style>