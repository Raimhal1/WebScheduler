<template>
  <div class="dialog" v-if="show" @click="hideDialog">
    <div class="dialog__content" @click.stop>
      <slot></slot>
    </div>
  </div>
</template>

<script>
import toggleEventFormMixin from "@/mixins/toggleEventFormMixin";

export default {
  name: "MyDialog",
  mixins: [toggleEventFormMixin]
}
</script>

<style scoped>
.dialog{
  top: 0;
  right: 0;
  left: 0;
  bottom: 0;
  background: rgba(0, 0, 0, 0.5);
  position: fixed;
  display: flex;
  z-index: 100;
}

.dialog__content{
  margin: auto;
  background: white;
  border-radius: 5px;
}
</style>