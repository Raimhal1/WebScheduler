<template>
  <my-input
      type="text"
      placeholder="Search"
      class="search__field"
  />
</template>

<script>
export default {
  name: "MySearch"
}
</script>

<style scoped>
.search__field{
  display: flex;
  width: 100%;
}
</style>