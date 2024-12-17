import React from 'react';
import { Text, View, Image } from "@react-pdf/renderer";
import { style } from '../../styles/PdfStyle';

const PdfHeader = () => {
  return (
    <View style={[style.center, {width: '100%'}]}>
      <View style={[style.flexRow, {width: '96%', marginTop: 10}]}>
        <View style={{ width: '70%', height: 50, borderRight: 1, borderColor: '#07DDA5', marginRight: 5 }}>
          <Image style={{width: '60%'}} src={"/images/Logo_Estructura.png"} />
        </View>
        <View style={{ width: '30%', height: 50, borderLeft: 1, borderColor: '#07DDA5', alignItems: "flex-end" }}>
          <Text style={[style.label, {paddingLeft: 10, fontSize: 12}]}>
            Av. 2 #30 Int. 1
            Delegación Coyoacán
            Colonia Educación
            C.P. 04400
          </Text>
        </View>
      </View>
    </View>
  )
}

export default PdfHeader;