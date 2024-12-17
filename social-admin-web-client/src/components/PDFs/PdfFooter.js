import React from 'react';
import { Text, View, Image } from "@react-pdf/renderer";
import { style } from '../../styles/PdfStyle';

const PdfFooter = () => {
  return (
    <View style={[style.center, { width: '90%', marginBottom: 10}]}>
      <View style={{ width: '100%', borderBottom: 1, borderColor: '#07DDA5', marginBottom: 5 }} />
      <View style={[style.center, { width: '100%', borderTop: 1, borderColor: '#07DDA5' }]}>
        <Text style={[style.label, { fontSize: 14 }]}>
          www.estructuraempresarial.com.mx jaja
        </Text>
      </View>
    </View>
  )
}

export default PdfFooter