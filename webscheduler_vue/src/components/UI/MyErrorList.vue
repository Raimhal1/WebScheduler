<template>
  <template  v-if="errors.length">
    <ol>
      <li v-for="message in errors" :key="message" class="error">{{ message }}</li>
    </ol>
  </template>
</template>

<script>

export default {
  name: "MyErrorList",
  props: {
    errors: {
      type: Array,
      required: true
    }
  },
}
</script>

<style scoped>
li {
  list-style-type: none;
}
</style>