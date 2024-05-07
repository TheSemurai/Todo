import React from "react";
import axios from "axios";

class HttpService extends React.Component {
  constructor(baseUrl = "https://localhost:44361/api") {
    super();
    this.baseUrl = baseUrl;
    this.instance = axios.create({ baseURL: this.baseUrl });
  }

  get defaultHeaders() {
    return {
      Authorization: localStorage.getItem("token"),
    };
  }

  async request(method, url, data = null, customHeaders = {}) {
    const headers = { ...this.defaultHeaders, ...customHeaders };
    const source = axios.CancelToken.source();

    const config = {
      method,
      url,
      headers,
      cancelToken: source.token,
    };

    if (data) {
      config.data = data;
    }

    return this.instance(config);
  }

  get(url, customHeaders = {}) {
    return this.request("get", url, null, customHeaders);
  }

  post(url, data, customHeaders = {}) {
    return this.request("post", url, data, customHeaders);
  }

  put(url, data, customHeaders = {}) {
    return this.request("put", url, data, customHeaders);
  }

  patch(url, data, customHeaders = {}) {
    return this.request("patch", url, data, customHeaders);
  }

  delete(url, customHeaders = {}) {
    return this.request("delete", url, null, customHeaders);
  }
}

export default HttpService;
