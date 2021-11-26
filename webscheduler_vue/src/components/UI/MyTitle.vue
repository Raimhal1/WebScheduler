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
  background: rgba(20, 121, 210, 0.66);
  color: azure;
  padding: 10px;
}
</style>