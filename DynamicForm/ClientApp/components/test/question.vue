<template>
  <div class="page-title">
    <h1>{{ data.displayName }}</h1>
  </div>
</template>

<script>
export default {
  props: {
    title: {
      type: String,
      default: 'Question'
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
  }
}
</script>
