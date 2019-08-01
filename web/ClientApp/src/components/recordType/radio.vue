<template>
    <div class="page-title">
        <div v-for="item in data.displayName">
            <input type="radio" :id="item.name" :value="item.id" v-model="data.id">
            <label :for="item.name">{{item.name}}( {{item.id}} ) </label>
        </div>

        <span>Picked: {{ data.value }}</span>
    </div>
</template>

<script>
    export default {
        props: {
            data: {
                type: Object,
                default: () => {
                    return {
                        id: 'qweqweqweqweqw',
                        type: 'radio',
                        displayName: [{name: 'one', id: '1'}, {name: 'two', id: '2'}, {name: 'aaa', id: '3'}],
                        //value: ,
                    };
                },
            },
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
