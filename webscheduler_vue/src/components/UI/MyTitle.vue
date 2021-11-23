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
  background: #0c20a1;
  color: azure;
  padding: 10px;
}
</style>