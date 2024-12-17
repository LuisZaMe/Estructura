import React from 'react';
import { PDFDownloadLink, PDFViewer } from "@react-pdf/renderer";
import DocuPDF from '../../services/CreatePdfService';

const ViewPdf = () => {
  return (
    <PDFViewer style={{ width: "100%", height: "90vh" }}>
      <DocuPDF />
    </PDFViewer>
  )
}

export default ViewPdf