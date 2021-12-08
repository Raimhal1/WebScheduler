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
  background: rgba(109, 165, 252, 0.9);
  color: azure;
  padding: 10px;
}
</style>