<template>
  <button class="btn">
    <slot></slot>
  </button>
</template>

<script>
export default {
  name: "my-button"
}
</script>

<style scoped>
.btn{
  border-color: #0c20a1;
  margin-top: 5px;
  background: none;
  padding: 10px 10px;
  color: #0c20a1;
}

</style>