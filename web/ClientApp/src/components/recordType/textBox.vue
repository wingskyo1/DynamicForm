<template>
  <div>
    <textarea v-model="data.value"></textarea>
  </div>
</template>

<script>
  export default {
    props: {
      title: {
        type: String,
        default: '填入答案區',
      },
      data: {
        // id: '',
        // type: 'textBox',
        // displayName: '',
        // value: '',
        type: Object,
      },
    },
  };
</script>
