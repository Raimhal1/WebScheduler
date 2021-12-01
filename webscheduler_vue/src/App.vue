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
    if(JSON.parse(localStorage.getItem('isAuth'))){
      this.$store.commit('setTokens', {
        access: localStorage.getItem('accessToken'),
        refresh: localStorage.getItem('refreshToken')
      })
      this.$store.commit('setAuth', JSON.parse(localStorage.getItem('isAuth')))
      this.$store.commit('setAdmin', JSON.parse(localStorage.getItem('isAdmin')))
      console.log(this.$store.state.isAuth)
      console.log(this.$store.state.isAdmin)
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