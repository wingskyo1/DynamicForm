<template>
  <div class="page-title">
    <select v-model="data.id">
      <option value="">請選擇</option>
      <option v-for="item in data.displayName" :value="item.id">{{item.name}}</option>
    </select>
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
            type: 'dropDown',
            displayName: [{name: 'one', id: '1'}, {name: 'two', id: '2'}, {name: 'aaa', id: '3'}],
            //value: ,
          };
        },
      },
    },
    created(){
      console.log(this.data.id);
    },
    data() {
      return {
        picked: '',
      };
    },
    watch: {
      clonedItems: function (newVal, oldVal) {
        //console.log('New value: ' + newVal + ', Old value: ' + oldVal);
        this.data.value = this.data.id;
        this.$emit('onChange', newVal, true);
        this.$emit('onChange', oldVal, false);
      },
    },
    computed:{
      clonedItems: function(){
        return JSON.parse(JSON.stringify(this.data.id))
      }
    },
  };
</script>

