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
        type: Object,
        default: () => {
          return {
            id: '',
            type: 'checkBox',
            displayName: '',
            value: '',
          };
        },
      },
    },
  };
</script>
