<template>
  <input :value='modelValue' @input="updateInput"  class="input">
</template>

<script>
export default {
  name: "my-input",
  props: {
    modelValue: [String, Number],
  },
  methods: {
    updateInput(event){
      this.$emit('update:modelValue', event.target.value)
    }
  }
}

</script>

<style scoped>

.input {
  border: 1px solid #0c20a1;
  padding: 10px 15px;
  margin-top: 10px;
  margin-bottom: 10px;
}
</style>