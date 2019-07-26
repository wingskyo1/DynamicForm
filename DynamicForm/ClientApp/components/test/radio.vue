<template>
  <div class="page-title">
    <label>
      <input type="checkbox" v-model="data.value" @change="onChange">
      <span class="checkable">{{data.displayName}} ( {{data.id}} ) </span>
    </label>
  </div>
</template>

<script>
  export default {
    props: {
      data: {
        type: Object,
        default: () => {
          return {
            id: '',
            type: 'radio',
            displayName: '',
            value: false,
          };
        },
      },
    },
    methods: {
      onChange: function () {
        this.$emit('onChange',this.data.id , this.data.value);
      },
    },
  };
</script>
