<template>
  <component v-show="data.show"  v-bind:is="data.type"

             :data="data"
             @onChange="onChange"
  ></component>
</template>

<script>
  import checkBox from './checkBox.vue';
  import question from './question.vue';
  import textBox from './textBox.vue';

  export default {
    props: {
      data: {
        type: Object,
        default: () => {
          return {
            id: '',
            type: 'checkBox',
            displayName: '',
            value: '',
            show: true,
          };
        },
      },
    },
    data() {
      return {
        text: '填入答案區',
        name: 'for Test',
      };
    },
    components: {
      'checkBox': checkBox,
      'question': question,
      'textBox': textBox,
    },
    methods: {
      onChange: function (id, value) {
        //console.log(id);
        this.$emit('answerChange', id, value);
      }
    }
  };
</script>
