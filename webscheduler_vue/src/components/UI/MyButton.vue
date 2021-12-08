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
  border-radius: 5px;
  margin-top: 5px;
  background: none;
  padding: 10px 10px;
  color: #0c20a1;
  cursor: pointer;
}
.btn:hover {
  background-color: #0c20a1;
  color: white;
  transition: 0.5s;
}
.btn:active {
  background-color: white !important;
  color: #0c20a1;
}
.btn:focus-visible {
  box-shadow: 0 0 0 3px lightskyblue;
}
.btn:disabled {
  background-color: #6c87b5;
  pointer-events: none;
}

</style>