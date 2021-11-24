<template>
  <h2 class="my__title"><slot></slot></h2>
</template>

<script>
export default {
  name: "my-title"
}
</script>

<style scoped>
.my__title{
  background: #00124e;
  color: azure;
  padding: 10px;
}
</style>