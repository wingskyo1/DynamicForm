<template>
  <div class="flex four">
    <div class="three-fourth">
      <input type="text" placeholder="輸入內容" v-model="data.name">
    </div>
    <div>
      <button class='error' @click="delRecord(data.id)">Del</button>
    </div>
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
            name: '',
          };
        },
      },
    },
    methods: {
      delRecord: function (id) {
        this.$emit('delRecord', id);
      },
    },
    created(){
      console.log(this.data)
    }
  };
</script>
