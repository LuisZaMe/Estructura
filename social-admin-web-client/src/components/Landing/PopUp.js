import React, { useState, useEffect } from 'react';

const Popup = ({ text, onClose }) => {
  const [visible, setVisible] = useState(true);

  useEffect(() => {
    if (visible) {
      const timeoutId = setTimeout(() => {
        setVisible(false);
      }, 5000);

      return () => {
        clearTimeout(timeoutId);
      };
    }
  }, [visible]);

  return (
    visible && (
      <div className="popup">
        <div className="popup-content">
          <button className="close-button" onClick={onClose}>
            X
          </button>
          <p className="popup-text">{text}</p>
        </div>
      </div>
    )
  );
};

export default Popup;