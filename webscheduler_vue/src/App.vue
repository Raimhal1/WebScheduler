<template>
    <navbar></navbar>
    <div class="app">
      <router-view></router-view>
    </div>
</template>

<script>

export default {
  mounted() {
    console.log('mounted')
    if(localStorage.isAuth){
      this.$store.commit('setTokens', {
        access: localStorage.accessToken,
        refresh: localStorage.refreshToken
      })
      this.$store.state.isAuth = localStorage.isAuth
      this.$store.state.isAdmin = localStorage.isAdmin
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

.user__form{
  width: 400px;
  display: flex;
  flex-direction: column;
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