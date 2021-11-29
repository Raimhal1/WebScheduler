<template>
  <select v-model="value" @change="changeOption" class="search">
    <option value="" disabled>Select from the list</option>
    <option
        v-for="option in options"
        :key="option.value"
        :value="option.value"
    >
      {{ option.name }}
    </option>
  </select>
</template>

<script>
export default {
  name: "my-select",
  props:{
    modelValue: [String],
    options:{
      type: Array,
      default: () => []
    }
  },
  data(){
    return{
      value: this.modelValue
    }
  },
  methods:{
    changeOption(event){
      this.$emit('update:modelValue', event.target.value)
    }

  }

}
</script>

<style scoped>
.search{
  height: 30px;
}
</style>